var options = {
    valueNames: ['ProductId', 'ProductName', 'ProductDescription', 'ProductPrice']
    }
    , documentTable = new List('AdminProductsTable', options);

$($('th.sort')[0]).trigger('click', function () {
    console.log('clicked');
});

$('input.search').on('keyup', function (e) {
    if (e.keyCode === 27) {
        $(e.currentTarget).val('');
        documentTable.search('');
    }
});