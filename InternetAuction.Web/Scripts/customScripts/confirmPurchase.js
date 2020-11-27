function confirmPurchase(id, isBuyClicked) {
    var buySpan = "buySpan_" + id;
    var confirmBuySpan = "confirmBuySpan_" + id;

    if (isBuyClicked) {
        $('#' + buySpan).hide();
        $('#' + confirmBuySpan).show();
    } else {
        $('#' + buySpan).show();
        $('#' + confirmBuySpan).hide();
    }
}