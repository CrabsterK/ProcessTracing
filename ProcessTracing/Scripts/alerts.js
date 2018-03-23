function showNewActivityAlert() {
    $(document).ready(function () {

        $('#someBtn').click(function () {
            $('#newActivityAlert').show('fade');
            $('#newActivityImg').show('fade');
            $('#someBtn').hide('fade');
        });
    });
}

function closeNewActivityAlert() {
    $(document).ready(function () {
        $('#linkClose').click(function () {
            $('#newActivityAlert').hide('fade');
            $('#newActivityImg').hide('fade');
            $('#someBtn').show('fade');
        })
    });
}