function adicionaGrafico(tabelaNome, containerNome) {
    //guarda referencia da tabela e id
    var idTabela = "#" + tabelaNome;
    var idContainer = "#" + containerNome;
    var tabela = $(idTabela);

    //pega os titulos das duas colunas da tabela
    var titulo = $(idTabela).find('th:nth-child(1)').html();
    var quantidade = $(idTabela).find('th:nth-child(2)').html();

    function drawChart() {
        // instancia tabela de dados para o grafico
        var dadosDoGrafico = new google.visualization.DataTable();

        // adiciona titulos 
        dadosDoGrafico.addColumn('string', titulo);
        dadosDoGrafico.addColumn('number', quantidade);

        //adiciona valores de cada linha da tabela
        $(idTabela + " > tbody > tr").each(function () {
            var valorTitulo = $(this).find('td:nth-child(1)').html();
            var valorQuantidade = parseInt($(this).find('td:nth-child(2)').html());
            dadosDoGrafico.addRow([valorTitulo, valorQuantidade]);
        });

        //Configs e invocacao do metodo .draw()
        var options = {
            'title': titulo,
            'width': 680,
            'height': 300,
            'colors': ['#ED145D', '#000', '#fc2a70', '#d8044b', '#5e5e5e']
        };
        var chart = new google.visualization.PieChart(document.getElementById(containerNome));
        chart.draw(dadosDoGrafico, options);
    };
    google.charts.setOnLoadCallback(drawChart);
};