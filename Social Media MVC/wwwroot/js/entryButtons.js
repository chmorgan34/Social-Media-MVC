
function copyLinkToClipboard(copyBtn) {
    var linkEl = $(copyBtn).siblings('.shareLink')[0];
    var popover = bootstrap.Popover.getInstance(linkEl);

    linkEl.select();
    document.execCommand('copy');
    popover.show();
}

function save(saveBtn) {
    var entryUrl = $(saveBtn).data('url');
    var entryId = $(saveBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            $('.saveBtn_' + entryId).addClass('d-none');
            $('.unsaveBtn_' + entryId).removeClass('d-none');
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
function unsave(unsaveBtn) {
    var entryUrl = $(unsaveBtn).data('url');
    var entryId = $(unsaveBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            $('.unsaveBtn_' + entryId).addClass('d-none');
            $('.saveBtn_' + entryId).removeClass('d-none');
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

function hide(hideBtn) {
    var entryUrl = $(hideBtn).data('url');
    var entryId = $(hideBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            $('.hideBtn_' + entryId).addClass('d-none');
            $('.unhideBtn_' + entryId).removeClass('d-none');
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
function unhide(unhideBtn) {
    var entryUrl = $(unhideBtn).data('url');
    var entryId = $(unhideBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            $('.unhideBtn_' + entryId).addClass('d-none');
            $('.hideBtn_' + entryId).removeClass('d-none');
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
function hideAndCollapse(unhideBtn) {
    var entryUrl = $(unhideBtn).data('url');
    var entryId = $(unhideBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            $('.entry-card_' + entryId).slideUp(function () {
                $('.entry-card_' + entryId).addClass('d-none');
            });
            try {
                if ($(selectedEntryAnchor).data('id') == entryId) {
                    $('#entryDetails').addClass('invisible');
                }
            } catch (error) {
                if (error instanceof ReferenceError) {
                    // In Details, do nothing
                }
            }
            
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

function edit(editBtn) {
    var id = $(editBtn).data('id');
    $('#content_' + id).addClass('d-none');
    $('#edit_' + id).removeClass('d-none');
    $('#edit_' + id).find('textarea')[0].select();
}
function saveEdit(saveEditBtn) {
    var id = $(saveEditBtn).data('id');
    var url = $(saveEditBtn).data('url');
    var textarea = $(saveEditBtn).siblings('textarea')[0];
    var content = $(textarea).val().trim();

    if (content == "" || content == null) {
        var textarea = $(saveEditBtn).siblings('textarea')[0];
        var popover = bootstrap.Popover.getInstance(textarea);
        popover.show();

        return;
    }

    $.ajax({
        method: 'POST',
        url: url,
        data: { id: id, content: content },
        success: function () {
            $('#content_' + id).html(content);
            $('#edit_' + id).addClass('d-none');
            $('#content_' + id).removeClass('d-none');
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
function cancelEdit(cancelBtn) {
    var id = $(cancelBtn).data('id');
    var textarea = $(cancelBtn).siblings('textarea')[0];
    $(textarea).val($('#content_' + id).html());
    $('#edit_' + id).addClass('d-none');
    $('#content_' + id).removeClass('d-none');
}

function deleteAndRedirect(deleteBtn) {
    var entryUrl = $(deleteBtn).data('url');
    var entryId = $(deleteBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            window.location.href = '/Home/Index';
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
function deleteAndCollapse(deleteBtn) {
    var entryUrl = $(deleteBtn).data('url');
    var entryId = $(deleteBtn).data('id');
    $.ajax({
        method: 'POST',
        url: entryUrl,
        data: { id: entryId },
        success: function () {
            var modalEl = document.getElementById('delete_' + entryId);
            var modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();
            $('.entry-card_' + entryId).slideUp(function () {
                $('.entry-card_' + entryId).addClass('d-none');
            });
            if ($(selectedEntryAnchor).data('id') == entryId) {
                $('#entryDetails').addClass('invisible');
            }
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

function reply(replyBtn, isHidden) {
    var id = $(replyBtn).data('id');
    var url = $(replyBtn).data('url');
    var textarea = $(replyBtn).siblings('textarea')[0];
    var content = $(textarea).val().trim();

    if (content == "" || content == null) {
        var textarea = $(replyBtn).siblings('textarea')[0];
        var popover = bootstrap.Popover.getInstance(textarea);
        popover.show();

        return;
    }

    $.ajax({
        method: 'POST',
        url: url,
        data: { id: id, content: content },
        success: function (commentId) {
            window.location.href = '/Entries/Details/' + commentId + '?showHidden=' + isHidden;
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
