google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(pageLoaded);

var periodVal = $('.btnGrpPeriodForSales .btn-primary[name="period"]').val();
var paramVal = 'Sum';

function pageLoaded() {
    $('.btnGrpPeriodForSales [name="period"]').click(function () {
        periodVal = this.value;

        var elem = $(this);

        paramVal = elem.parent().data('param');

        elem.siblings()
            .removeClass('btn-primary')
            .addClass('btn-default');

        elem.removeClass('btn-default')
            .addClass('btn-primary');

        drawSalesChart();
    });

    $('.btnGrpStockLeftovers').click(function () {
        paramVal = $(this).data('param');

        drawStockLeftoversChart();
    });

    drawSalesChart();
    drawStockLeftoversChart();

    paramVal = 'Quantity';
    drawSalesChart();
    drawStockLeftoversChart();
}

function drawSalesChart() {
    var loadGif = $(`.btnGrpPeriodForSales[data-param=${paramVal}]`).siblings('.ajax-load');
    var jsonData = $.ajax({
        url: "Reports/AjaxGetSalesSumsOrQtyByPeriod",
        data: { period: periodVal, param: paramVal },
        dataType: "json",
        async: false,
        beforeSend: function () {
            loadGif.css('visibility', 'visible');
        },
        complete: function () {
            setTimeout(function () {
                loadGif.css('visibility', 'hidden');
            }, 500);
        }
    }).responseJSON;

    var donutChart = document.getElementById(paramVal == 'Sum' ? 'chartSalesSums' : 'chartSalesQty');

    if (jsonData.rows.length == 0) {
        donutChart.innerHTML = 'Нет данных за данный период';
        return;
    } // if

    var data = new google.visualization.DataTable(jsonData);

    var options = {
        title: `${paramVal == 'Sum' ? 'Сумма' : 'Количество'} продаж по категориям`,
        pieHole: 0.4
    };

    var chart = new google.visualization.PieChart(donutChart);
    chart.draw(data, options);
}

function drawStockLeftoversChart() {
    var loadGif = $(`.btnGrpStockLeftovers[data-param=${paramVal}]`).siblings('.ajax-load');
    var jsonData = $.ajax({
        url: "Reports/AjaxGetStockLeftoversSumsOrQty",
        data: { param: paramVal },
        dataType: "json",
        async: false,
        beforeSend: function () {
            loadGif.css('visibility', 'visible');
        },
        complete: function () {
            setTimeout(function () {
                loadGif.css('visibility', 'hidden');
            }, 500);
        }
    }).responseJSON;

    var donutChart = document.getElementById(paramVal == 'Sum' ?
        'chartStockLeftoversSums' : 'chartStockLeftoversQty');

    if (jsonData.rows.length == 0) {
        donutChart.innerHTML = 'Нет данных для отображения';
        return;
    } // if

    var data = new google.visualization.DataTable(jsonData);

    var options = {
        title: `${paramVal == 'Sum' ? 'Сумма' : 'Количество'} остатков по категориям`,
        pieHole: 0.4
    };

    var chart = new google.visualization.PieChart(donutChart);
    chart.draw(data, options);
}