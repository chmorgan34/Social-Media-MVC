
function setup() {
    // Format DateTimes
    $('time.timeago').timeago();

    // Initialize share link popovers
    var shareLinkEls = [].slice.call(document.querySelectorAll('.shareLink'));
    shareLinkEls.map(function (el) {
        if (bootstrap.Popover.getInstance(el) == null) {
            var popover = new bootstrap.Popover(el, {
                content: 'Copied!',
                placement: 'top',
                trigger: 'manual'
            });
            el.addEventListener('shown.bs.popover', function () {
                setTimeout(function () {
                    popover.hide();
                }, 1500);
            });
        }
    });

    // Edit validation popovers
    var editTextAreas = [].slice.call(document.querySelectorAll('.textInput'));
    editTextAreas.map(function (el) {
        if (bootstrap.Popover.getInstance(el) == null) {
            var popover = new bootstrap.Popover(el, {
                content: "You forgot something...",
                placement: 'top',
                trigger: 'manual'
            });
            el.addEventListener('shown.bs.popover', function () {
                setTimeout(function () {
                    popover.hide();
                }, 3000);
            });
        }
    });

    // Ensure modals show correctly (not in background)
    $('.modal').appendTo('body');
}

function scrollToEntry(id) {
    var entryCards = $('.entry-card_' + id).get()
    if (entryCards.length == 2) {
        entryCards[1].scrollIntoView();
        $(entryCards[1]).addClass('entry-selected');
    }
    else if (entryCards.length == 1) {
        entryCards[0].scrollIntoView();
        $(entryCards[0]).addClass('entry-selected');
    }
}

setup();
