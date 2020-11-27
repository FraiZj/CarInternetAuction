function confirmSale(id, isSellClicked) {
    var saleSpan = "saleSpan_" + id;
    var confirmSaleSpan = "confirmSaleSpan_" + id;

    if (isSellClicked) {
        $('#' + saleSpan).hide();
        $('#' + confirmSaleSpan).show();
    } else {
        $('#' + saleSpan).show();
        $('#' + confirmSaleSpan).hide();
    }
}