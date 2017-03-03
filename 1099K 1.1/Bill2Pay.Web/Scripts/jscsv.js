jQuery.fn.tableToCSV = function (name) {
    var clean_text = function (text) {
        text = text.replace(/ /g, '');
        text = text.replace(/\n/g, '');
        text = text.replace(/"/g, '""');
        
        return '"' + text + '"';
    };

    $(this).each(function () {
        var table = $(this);
        var caption = $(this).find('caption').text();
        var title = [];
        var rows = [];

        $(this).find('tr').each(function () {
            var data = [];
            $(this).find('th').each(function () {
                var text = clean_text($(this).text());
                title.push(text);
            });
            $(this).find('td').each(function () {
                var text = clean_text($(this).text());
                data.push(text);
            });
            data = data.join(",");

            rows.push(data);
        });
        title = title.join(",");
        rows = rows.join("\n");
        var ts = new Date().getTime();
        name = name + ts;
        var csv = title + rows;
        var blob = new Blob([csv], { type: "text/csv" });
        // Determine which approach to take for the download
        if (navigator.msSaveOrOpenBlob) {
            // Works for Internet Explorer and Microsoft Edge
            navigator.msSaveOrOpenBlob(blob, name+".csv");

        } else {

            // Attempt to use an alternative method
            var anchor = document.body.appendChild(
              document.createElement("a")
            );
            // If the [download] attribute is supported, try to use it
            if ("download" in anchor) {
                anchor.download = name+".csv";
                anchor.href = URL.createObjectURL(blob);
                anchor.click();
            }

        }
    });

};
