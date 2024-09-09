$(document).ready(function () {
    $('.btnViewDetail').click(function () {
        var id = $(this).data('id');
        $.ajax({
            method: "GET",
            url: "/Product/GetDetail",
            data: { productid: id },
            success: function (data) {
                if (data.Product) {
                    $('#Product-HinhAnh').html("<img width='150' src='/Assets/Images/" + data.Product.MALK + ".jpg'>");
                    $("#idhref").attr("href", `/Cart/ThemGioHang?MALK=${data.Product.MALK}`);
                    $('#Product-nAme').text(data.Product.TENLK);
                    $('#GiaTien').text(data.Product.DONGIA);
                    $('#NamSanXuat').text(data.Product.NAMSX);
                    $('#SoLuongTon').text(data.Product.SOLUONGTON);
                    
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
});
