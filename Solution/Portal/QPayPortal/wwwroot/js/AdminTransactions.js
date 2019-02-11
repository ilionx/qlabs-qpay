var options = {
    valueNames: ['TransactionId', 'EmployeeEmail', 'DateTime', 'ProductId','Amount', 'TransactionType', 'ProviderName', 'ProviderTransactionId']
    }
    , documentTable = new List('AdminTransactionTable', options);

$($('th.sort')[0]).trigger('click', function () {
    console.log('clicked');
});

$('input.search').on('keyup', function (e) {
    if (e.keyCode === 27) {
        $(e.currentTarget).val('');
        documentTable.search('');
    }
});

$(document).ready(function () {
    $('#example').DataTable({
        columnDefs: [
            {
                targets: [0, 1, 2],
                className: 'mdl-data-table__cell--non-numeric'
            }
        ]
    });
});