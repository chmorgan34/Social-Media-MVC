
function upvote(upvoteBtn) {
    var downvoteBtn = $(upvoteBtn).siblings('.downvoteBtn')[0];
    if ($(downvoteBtn).hasClass('d-none')) {
        // Is downvoted, undo downvote
        var unDownvoteBtn = $(downvoteBtn).siblings('.unDownvoteBtn')[0];
        unDownvote(unDownvoteBtn);
    }

    var entryId = $(upvoteBtn).data('id');
    $.ajax({
        method: 'POST',
        url: '/Entries/Upvote',
        data: { id: entryId },
        async: false,
        success: function () {
            var unUpvoteBtns = $('.entry-card_' + entryId).find('.unUpvoteBtn');
            var voteScoreSpans = $('.entry-card_' + entryId).find('.voteScore');
            var voteScore = parseInt($(voteScoreSpans[0]).html());
            var newVoteScore = voteScore + 1;

            $(unUpvoteBtns).removeClass('d-none');
            $('.entry-card_' + entryId).find('.upvoteBtn').addClass('d-none');
            $(voteScoreSpans).html('' + newVoteScore);
        },
        error: function (xhr, status, error) {
            switch (xhr.status) {
                case 401:
                    window.location.href = '/Identity/Account/Login';
                    break;
                default:
                    alert(xhr.status);
                    break;
            }
        }
    });
}
function unUpvote(unUpvoteBtn) {
    var entryId = $(unUpvoteBtn).data('id');
    $.ajax({
        method: 'POST',
        url: '/Entries/RemoveUpvote',
        data: { id: entryId },
        async: false,
        success: function () {
            var upvoteBtns = $('.entry-card_' + entryId).find('.upvoteBtn');
            var voteScoreSpans = $('.entry-card_' + entryId).find('.voteScore');
            var voteScore = parseInt($(voteScoreSpans[0]).html());
            var newVoteScore = voteScore - 1;

            $(upvoteBtns).removeClass('d-none');
            $('.entry-card_' + entryId).find('.unUpvoteBtn').addClass('d-none');
            $(voteScoreSpans).html('' + newVoteScore);
        },
        error: function (xhr, status, error) {
            switch (xhr.status) {
                case 401:
                    window.location.href = '/Identity/Account/Login';
                    break;
                default:
                    alert(xhr.status);
                    break;
            }
        }
    });
}


function downvote(downvoteBtn) {
    var upvoteBtn = $(downvoteBtn).siblings('.upvoteBtn')[0];
    if ($(upvoteBtn).hasClass('d-none')) {
        // Is upvoted, undo upvote
        var unUpvoteBtn = $(upvoteBtn).siblings('.unUpvoteBtn')[0];
        unUpvote(unUpvoteBtn);
    }

    var entryId = $(downvoteBtn).data('id');
    $.ajax({
        method: 'POST',
        url: '/Entries/Downvote',
        data: { id: entryId },
        async: false,
        success: function () {
            var unDownvoteBtns = $('.entry-card_' + entryId).find('.unDownvoteBtn');
            var voteScoreSpans = $('.entry-card_' + entryId).find('.voteScore');
            var voteScore = parseInt($(voteScoreSpans[0]).html());
            var newVoteScore = voteScore - 1;

            $(unDownvoteBtns).removeClass('d-none');
            $('.entry-card_' + entryId).find('.downvoteBtn').addClass('d-none');
            $(voteScoreSpans).html('' + newVoteScore);
        },
        error: function (xhr, status, error) {
            switch (xhr.status) {
                case 401:
                    window.location.href = '/Identity/Account/Login';
                    break;
                default:
                    alert(xhr.status);
                    break;
            }
        }
    });
}
function unDownvote(unDownvoteBtn) {
    var entryId = $(unDownvoteBtn).data('id');
    $.ajax({
        method: 'POST',
        url: '/Entries/RemoveDownvote',
        data: { id: entryId },
        async: false,
        success: function () {
            var downvoteBtns = $('.entry-card_' + entryId).find('.downvoteBtn');
            var voteScoreSpans = $('.entry-card_' + entryId).find('.voteScore');
            var voteScore = parseInt($(voteScoreSpans[0]).html());
            var newVoteScore = voteScore + 1;

            $(downvoteBtns).removeClass('d-none');
            $('.entry-card_' + entryId).find('.unDownvoteBtn').addClass('d-none');
            $(voteScoreSpans).html('' + newVoteScore);
        },
        error: function (xhr, status, error) {
            switch (xhr.status) {
                case 401:
                    window.location.href = '/Identity/Account/Login';
                    break;
                default:
                    alert(xhr.status);
                    break;
            }
        }
    });
}

