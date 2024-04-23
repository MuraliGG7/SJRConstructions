// Function to show message in modal
function showMessage(message, messageType) {
    const modal = $('#messageModal');
    const messageContent = $('#messageContent');

    messageContent.empty(); // Clear existing content

    if (messageType === 'success') {
        modal.find('.modal-title').text('Success Message');
        messageContent.append('<div class="alert alert-success">' + message + '</div>');
    } else if (messageType === 'warning') {
        modal.find('.modal-title').text('Warning Message');
        messageContent.append('<div class="alert alert-warning">' + message + '</div>');
    }

    $('#showMessageModal').click(); // Show the modal
}
function showConfirmMessage(messageType) {   
    if (messageType === 'save') {          
        $('#cfrsavemessage').show();
        $('#cfrresetmessage').hide();
        $('#cfrdeletemessage').hide();
        $('#saveChangesBtn').show();
        $('#resetBtn').hide();
        $('#crfdeleteBtn').hide();
    } else if (messageType === 'reset') {
        $('#cfrsavemessage').hide();
        $('#cfrresetmessage').show();
        $('#cfrdeletemessage').hide();
        $('#saveChangesBtn').hide();
        $('#resetBtn').show();
        $('#crfdeleteBtn').hide();
    } else if (messageType === 'delete') {
        $('#cfrsavemessage').hide();
        $('#cfrresetmessage').hide();
        $('#cfrdeletemessage').show();
        $('#saveChangesBtn').hide();
        $('#resetBtn').hide();
        $('#crfdeleteBtn').show();
    }

    $('#showCrfMessageModal').click(); // Show the modal
}