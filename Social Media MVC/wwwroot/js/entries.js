
var selectedEntryAnchor;


// Gives the selected entry a blue border
function updateSelectedEntry(entryAnchor) {
    if (selectedEntryAnchor != null) {
        var currentClass = '.entry-card_' + $(selectedEntryAnchor).data('id');
        $(currentClass).removeClass('entry-selected');
    }
    var selectedClass = '.entry-card_' + $(entryAnchor).data('id');
    $(selectedClass).addClass('entry-selected');
    selectedEntryAnchor = entryAnchor;
}

function showDetails(entryAnchor, isComment, showHidden) {
    // Use default behavior on small devices (open the link)
    if ($('#entryDetails').css('display') == 'none') {
        return true;
    }

    // Screen is large enough, show loading spinner
    $('#entryDetails').html(
        '<div class="d-flex align-items-center justify-content-center h-100" id="postDetailsResults">' +
            '<div class="spinner-border" role="status">' +
                '<span class="visually-hidden">Loading...</span>' +
            '</div>' +
        '</div'
    );
    $('#entryDetails').removeClass('invisible');

    // Fill #entryDetails with partial view
    var entryUrl = $(entryAnchor).data('url');
    var entryId = $(entryAnchor).data('id');
    $.ajax({
        method: 'GET',
        url: entryUrl,
        data: { id: entryId, showHidden: showHidden },
        success: function (data) {
            updateSelectedEntry(entryAnchor);
            $('#entryDetails').html(data);

            // No scripts in a partial view, so call setup() again for entry details
            setup();
            if (isComment) {
                scrollToEntry(entryId);
            }
        },
        error: function (xhr, status, error) {
            $('#postDetailsResults').html(xhr.status + ': ' + error);
        }
    });

    // Ignore default behavior (opening the link)
    return false;
}






