$('body').on('submit', '#form', (e) => handlePlaceBetRequest(e));

function handlePlaceBetRequest(e) {
    e.preventDefault();
    var form = $('#form');
    var url = form.attr('action');
    var type = form.attr('method');

    $.ajax({
        type: type,
        url: url,
        data: form.serialize(),
        success: function (data) {
            if (typeof data == "string") {
                var index = data.search("!DOCTYPE");

                if (index !== -1) {
                    document.open();
                    document.write(data);
                    document.close();
                    return;
                }

                form.parent("div").html(data);
            }
        }
    });
};