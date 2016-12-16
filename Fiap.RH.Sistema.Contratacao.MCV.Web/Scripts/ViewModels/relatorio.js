$().ready(function () {
    $("select").change(function () {
        var idOrdem = this.name;
        var idHabilidadeSelecionada = $("#" + idOrdem + " option:selected").attr("value");
        atualizarHabilidade(idOrdem, idHabilidadeSelecionada);
    });
});


$().ready(function () {

    $("#IdBusca").change(function () {
        $("#formularioBusca").submit();
    });

});

function removerHabilidade(idOrdem) {
    $.ajax({
        url: "http://localhost:4864/Relatorio/RemoverHabilidade",
        type: "POST",
        data: { id: idOrdem },
        success: function (json) {
            if (json.status) {
                $("#" + idOrdem).remove();
            } else {
                alert(json.mensagem);
            }
        },
        error: function (json) {
            alert("Erro ao se conectar com o servidor para remover!");
        }
    });
};

function atualizarHabilidade(idOrdem, idHabilidadeSelecionada) {
    $.ajax({
        url: "http://localhost:4864/Relatorio/AtualizarHabilidade",
        type: "POST",
        data: { id: idOrdem, idHabilidade: idHabilidadeSelecionada },
        success: function (json) {
            if (json.status) {
                console.log("Habilidade atualizada com sucesso!")
            } else {
                alert(json.mensagem);
            }
        },
        error: function (json) {
            alert("Erro ao se conectar com o servidor para atualizar a habilidade!");
        }
    });
};


