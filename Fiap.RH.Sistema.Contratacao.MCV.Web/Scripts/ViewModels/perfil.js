function removePeloId(idRemover) {
    $.ajax({
        url: "http://localhost:4864/Perfil/Remover",
        type: "POST",
        data: { id: idRemover },
        success: function (json) {
            if (json.status) {
                $("#" + idRemover).remove();
            } else {
                alert(json.mensagem);
            }
        },
        error: function (json) {
            alert("Erro ao se conectar com o servidor para remover!");
        }
    });
};
