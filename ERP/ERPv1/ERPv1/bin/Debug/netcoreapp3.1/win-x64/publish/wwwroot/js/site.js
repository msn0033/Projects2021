// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.date').datepicker({
        dateFormat: "DD, MM d, yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1940:2050"
    });
    $('.dateSamll').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1940:2050"
    });

    $('.dataTable').dataTable({
        "language": {
            "sProcessing": "جارٍ التحميل...",
            "sLengthMenu": "أظهر _MENU_ مدخلات",
            "sZeroRecords": "لم يعثر على أية سجلات",
            "sInfo": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مدخل",
            "sInfoEmpty": "يعرض 0 إلى 0 من أصل 0 سجل",
            "sInfoFiltered": "(منتقاة من مجموع _MAX_ مُدخل)",
            "sInfoPostFix": "",
            "sSearch": "ابحث:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "الأول",
                "sPrevious": "السابق",
                "sNext": "التالي",
                "sLast": "الأخير"
            }
        },
        "order": []
    });

    $('.dataTable tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" class="form-control" />');
    });


    //var table = $('.dataTable').DataTable({
    //    "language": {
    //        "sProcessing": "جارٍ التحميل...",
    //        "sLengthMenu": "أظهر _MENU_ مدخلات",
    //        "sZeroRecords": "لم يعثر على أية سجلات",
    //        "sInfo": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مدخل",
    //        "sInfoEmpty": "يعرض 0 إلى 0 من أصل 0 سجل",
    //        "sInfoFiltered": "(منتقاة من مجموع _MAX_ مُدخل)",
    //        "sInfoPostFix": "",
    //        "sSearch": "ابحث:",
    //        "sUrl": "",
    //        "oPaginate": {
    //            "sFirst": "الأول",
    //            "sPrevious": "السابق",
    //            "sNext": "التالي",
    //            "sLast": "الأخير"
    //        }
    //    },
    //        initComplete: function () {
    //            // Apply the search
    //            this.api().columns().every(function () {
    //                var that = this;

    //                $('input', this.footer()).on('keyup change clear', function () {
    //                    if (that.search() !== this.value) {
    //                        that
    //                            .search(this.value)
    //                            .draw();
    //                    }
    //                });
    //            });
    //    }
    //    ,
    //    "order": []
    //    });



    $('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
        if (!$(this).next().hasClass('show')) {
            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
        }
        var $subMenu = $(this).next(".dropdown-menu");
        $subMenu.toggleClass('show');


        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
            $('.dropdown-submenu .show').removeClass("show");
        });


        return false;
    });
});

