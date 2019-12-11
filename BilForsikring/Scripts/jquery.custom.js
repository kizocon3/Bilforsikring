/*!
 * jQuery Custom
 *
 * Copyright (c) 2019 Frank Campos Hernandez
 */

$(function () {

    $('#dinBonusInfo').on("click", function (evt) {
        evt.preventDefault();
        $("#dinBonusModal").modal('show');
    });

    $("#avbryBtn").on("click", function (evt) {
        evt.preventDefault();
        $("form input:text").val("");
        $("select#bonusDropDown").prop("selectedIndex", 0).val();
    });

    /*
    $("#beregnPrisBtn").on("click", function (evt) {
        evt.preventDefault();
        var epostvalue = $("#epostfield").val();
        if (!epostvalue)
        {
            $("#epostfield").addClass("makeEpostFielddRed");
        }
        else {
            $("#epostfield").removeClass("makeEpostFielddRed");
        }
    });
    */
});

   
 
