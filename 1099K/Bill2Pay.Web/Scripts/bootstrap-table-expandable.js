(function ($) {
    $(function () {
        $('.table-expandable').each(function () {
            var table = $(this);
           // table.children('thead').children('tr').append('<th></th>');
            table.children('tbody').children('tr').filter(':odd').hide();
            table.children('tbody').children('tr').filter(':even').click(function () {
                var element = $(this);
                element.next('tr').toggle('slow');
                element.find(".table-expandable-arrow").toggleClass("up");
            });
            table.children('tbody').children('tr').filter(':even').each(function () {
                var element = $(this);
                element.find("td:first").before('<td class="text-left tableBorder" ><div class="table-expandable-arrow"></div></td>');
            });
        });
    });
})(jQuery);
$(document).ready(function () {
   
    var rows = $('#mainTable tbody .record');
    for (var i = 0; i < rows.length; i++) {
        if (i % 2 == 0)
            $(rows[i]).css("background-color", "#F9F9F9");
        else
            $(rows[i]).css("background-color", "#fff");
    }
});