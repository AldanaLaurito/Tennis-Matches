var Scorer = function () {
    this.local = $('#local');
    this.visitante = $('#visitante');
    this.id = $('#Id');
    this.puntosLocal = $('#puntosLocal');
    this.gamesLocal = $('#gamesLocal');
    this.puntosVisitante = $('#puntosVisitante');
    this.gamesVisitante = $('#gamesVisitante');
};

Scorer.prototype.sumarPuntoLocal = function(){
    var self = this;
    self.local.click(function () {
        $.ajax({
            url: '/Tablero/SumarPuntoLocal/' + self.id.val(),
            type: 'POST',
            dataType: 'json'
        }).done(function (data) {
            if (data.success) {
                self.actualizarTabla(data.partido);
            }
        });
    });
};

Scorer.prototype.sumarPuntoVisitante = function () {
    var self = this;
    self.visitante.click(function () {
        $.ajax({
            url: '/Tablero/SumarPuntoVisitante/' + self.id.val(),
            type: 'POST',
            dataType: 'json'
        }).done(function (data) {
            if (data.success) {
                self.actualizarTabla(data.partido);
            }
        });
    });
};

Scorer.prototype.actualizarTabla = function (partido) {
    this.puntosLocal.html(partido.PuntosGameLocal);
    this.gamesLocal.html(partido.GamesLocal);
    this.puntosVisitante.html(partido.PuntosGameVisitante);
    this.gamesVisitante.html(partido.GamesVisitante);


    if (partido.Estado == 3) {
        this.local.hide();
        this.visitante.hide();
    } else {
        this.local.show();
        this.visitante.show();
    }
};

$(document).ready(function () {
    var scorer = new Scorer();

    scorer.sumarPuntoLocal();
    scorer.sumarPuntoVisitante();
});