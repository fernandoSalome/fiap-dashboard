$(document).ready(function () {
    adicionaGrafico("participantes", "participantes-grafico");
    adicionaGrafico("publico", "publico-grafico");
    adicionaGrafico("presentes", "presentes-grafico");
    adicionaGrafico("aprovados", "aprovados-grafico");
    adicionaGrafico("participantes-aprovados", "participantes-aprovados-grafico");
});

// Set a callback to run when the Google Visualization API is loaded.
google.charts.load('current', { 'packages': ['corechart'] });