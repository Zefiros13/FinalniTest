$(window).on('load', function () {
    $("#divRegistration").hide();
    $("#divLogin").hide();
    $("#btnLogOut").hide();

    $("#zaposleniForm").hide();
    $("#pretragaForm").hide();

    refreshTable();
});


$(document).ready(function () {
    var token = null;
    var headers = {};
    var formAction = "Create";
    var editingId;
    var endpoint = "https://localhost:44350/api/zaposleni/";

    $("body").on("click", "#btnDelete", deleteZaposlenog);

    $("#btnZaposleni").on("click", function () {
        $.getJSON(endpoint, ucitajZaposlene);
        headers.Authorization = "Bearer " + token;

        $.getJSON("https://localhost:44350/api/jedinice", populateDropDownList);
    });


    $("#btnRegister").on("click", function () {
        $("#divRegistration").show();
        $("#divLogin").hide();
        $("#LoginOrRegisterDiv").hide();
    });

    $("#btnLogin").on("click", function () {
        $("#divRegistration").hide();
        $("#divLogin").show();
        $("#LoginOrRegisterDiv").hide();
    });

    $("#odustajanjeReg").on("click", function () {
        $("#LoginOrRegisterDiv").show();
        $("#divRegistration").hide();
        $("#divLogin").hide();
    });

    $("#odustajanjeLog").on("click", function () {
        $("#LoginOrRegisterDiv").show();
        $("#divRegistration").hide();
        $("#divLogin").hide();
    });

    //LOGIN 
    $("#loginForm").submit(function (e) {
        e.preventDefault();

        var loginEmail = $("#loginEmail").val();
        var loginPassword = $("#loginPassword").val();

        var sendData = {
            "grant_type": "password",
            "username": loginEmail,
            "password": loginPassword
        };

        $.ajax({
            type: "POST",
            url: "https://localhost:44350/Token",
            data: sendData
        }).done(function (data, status) {
            token = data.access_token;

            $("#divLogin").hide();
            $("#LoginOrRegisterDiv").hide();
            $("#user").empty();
            $("#user").append("Loged in user: " + loginEmail);
            $("#btnLogOut").show();

            $("#loginEmail").val('');
            $("#loginPassword").val('');
            $("#btnLoginOrRegister").hide();

            $("#divZaposleni").show();
            $("#pretragaForm").show();
            refreshTable();
        }).fail(function (data, status) {
            alert("Greska prilikom prijave! " + data);
        });
    });

    //REGISTER
    $("#registerForm").submit(function (e) {
        e.preventDefault();

        var registerEmail = $("#registerEmail").val();
        var registerPassword = $("#registerPassword").val();

        var sendData = {
            "Email": registerEmail,
            "Password": registerPassword,
            "ConfirmPassword": registerPassword
        };

        $.ajax({
            type: "POST",
            url: "https://localhost:44350/api/Account/Register",
            data: sendData
        }).done(function (data) {
            $("#message").css("color", "green");
            $("#message").append("Registracija uspesna! Sada mozete da se prijavite");
            $("#divLogin").show();
            $("#divRegistration").hide();
            $("#loginEmail").val(registerEmail);
            $("#loginPassword").val(registerPassword);

            $("#registerEmail").val('');
            $("#registerPassword").val('');
            $("#registerConfirmPassword").val('');
        }).fail(function (data) {
            alert("Greska prilikom registracije! " + data);
        });
    });

    //LOG OUT
    $("#btnLogOut").on("click", function () {
        token = null;
        headers = {};

        $("#divLogin").hide();
        $("#divRegistration").hide();
        $("#LoginOrRegisterDiv").show();
        $("#user").empty();
        $("#message").empty();
        $("#btnLogOut").hide();
        $("#btnLoginOrRegister").show();

        $("#pretragaForm").hide();
        refreshTable();
    });


    //DROP DOWN
    function populateDropDownList(data, status) {
        var select = $("#jedinica");
        select.empty();
        var firstOption = $("<option value=0>----- Izaberi Jedinicu -----</option>");
        select.append(firstOption);

        if (status == "success") {
            for (var i = 0; i < data.length; i++) {
                var option = $("<option value=" + data[i].Id + ">" + data[i].Ime + "</option>");
                select.append(option);
            }
        }
        else {

        }
    }

    //PRETRAGA FORM
    $("#pretragaForm").submit(function (e) {
        e.preventDefault();

        var najmanje = parseInt($("#najmanje").val());
        var najvise = parseInt($("#najvise").val());

        if (Number.isNaN(najmanje)) {
            najmanje = 250;
        }
        if (Number.isNaN(najvise)) {
            najvise = 10000;
        }
        $.ajax({
            type: "POST",
            url: "api/pretraga/?najmanja=" + najmanje.toString() + "&najveca=" + najvise.toString()
        }).done(function (data, status) {
            ucitajZaposlene(data, status);
        }).fail(function (data, status) {
            alert("Greska tokom pretrage!");
        });

    });

    //FORM
    $("#zaposleniForm").submit(function (e) {
        e.preventDefault();

        var rola = $("#rola").val();
        var ImeIPrezime = $("#ImeIPrezime").val();
        var godinaRodjenja = parseInt($("#godinaRodjenja").val());
        var godinaZaposlenja = parseInt($("#godinaZaposlenja").val());
        var plata = parseInt($("#plata").val());
        var jedinica = parseInt($("#jedinica").val());

        var httpAction;
        var url;
        var sendData;

        if (formAction === "Create") {
            httpAction = "POST";
            url = endpoint;
            sendData = {
                "Rola": rola,
                "ImeIPrezime": ImeIPrezime,
                "GodinaRodjenja": godinaRodjenja,
                "GodinaZaposlenja": godinaZaposlenja,
                "Plata": plata,
                "JedinicaId": jedinica
            };
        }
        //kasno sam shvatio da ovo ne treba
        else {
            httpAction = "PUT";
            url = endpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Rola": rola,
                "ImeIPrezime": ImeIPrezime,
                "GodinaRodjenja": godinaRodjenja,
                "GodinaZaposlenja": godinaZaposlenja,
                "Plata": plata,
                "JedinicaId": jedinica
            };
        }

        $.ajax({
            type: httpAction,
            url: url,
            data: sendData,
            "headers": headers
        }).done(function (data, status) {
            refreshTable();
            formAction = "Create";
        }).fail(function (data, status) {
            formAction = "Create";
            alert("Greska prilikom dodavanja!");
        });
    });

    //UCITAVANJE
    function ucitajZaposlene(data, status) {
        var $container = $("#zaposleniTable");
        $container.empty();

        if (status == "success") {
            var div = $("<div></div>");
            var h1 = $("<h1>Zaposleni</h1>");
            div.append(h1);

            var table = $("<table class='table table-hover'></table>");
            var header = "<tr style='background-color:yellow;'><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Jedinica</td>";
            if (token) {
                header += "<td>Godina rodjenja</td><td>Plata</td><td>Akcija</td>";
            }
            header += "</tr >";
            table.append(header);

            for (var i = 0; i < data.length; i++) {
                var row = "<tr>";
                var displayData = "<td>" + data[i].ImeIPrezime + "</td><td>" + data[i].Rola + "</td><td>" + data[i].GodinaZaposlenja + "</td><td>" + data[i].Jedinica.Ime + "</td>";
                var stringId = data[i].Id.toString();
                var displayRodjenje = "<td>" + data[i].GodinaRodjenja + "</td>";
                var displayPlatu = "<td>" + data[i].Plata + "</td>";
                var displayDelete = "<td><button id=btnDelete name=" + stringId + " class='btn btn-danger'>Delete</button></td>";
                if (token) {
                    row += displayData + displayRodjenje + displayPlatu + displayDelete + "</tr>";
                    $("#zaposleniForm").show();
                }
                else {
                    row += displayData + "</td>";
                    $("#zaposleniForm").hide();
                }
                table.append(row);
            }
            div.append(table);
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Neuspesno ucitavanje</h1>");
            div.append(h1);
        }

        $container.append(div);
    }

    //DELETE
    function deleteZaposlenog() {
        var deleteID = this.name;

        $.ajax({
            type: "DELETE",
            url: endpoint + deleteID.toString(),
            "headers": headers
        }).done(function (data, status) {
            refreshTable();
        }).fail(function (data, status) {
            alert("Greska pri brisanju!");
        });
    }
});

//REFRESH
function refreshTable() {
    $("#jedinica").val(0);
    $("#rola").val('');
    $("#ImeIPrezime").val('');
    $("#godinaRodjenja").val('');
    $("#godinaZaposlenja").val('');
    $("#plata").val('');

    $("#btnZaposleni").trigger("click");
}
