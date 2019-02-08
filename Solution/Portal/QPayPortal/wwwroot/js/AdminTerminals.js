var options = {
    valueNames: ['TerminalId', 'ProductId', 'Productname', 'ProductDescription', 'ProductPrice']
    }
    , documentTable = new List('AdminTerminalTable', options);

$($('th.sort')[0]).trigger('click', function () {
    console.log('clicked');
});

$('input.search').on('keyup', function (e) {
    if (e.keyCode === 27) {
        $(e.currentTarget).val('');
        documentTable.search('');
    }
});