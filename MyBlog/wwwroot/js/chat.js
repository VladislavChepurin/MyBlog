"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


connection.on("ReceiveMessage", function (request) {
    
    var inputVoltage = document.getElementById(request.serialNumber + "-inputVoltage");
    inputVoltage.textContent = request.inputVoltage + " Вольт";

    var outputVoltage = document.getElementById(request.serialNumber + "-outputVoltage");
    outputVoltage.textContent = request.outputVoltage + " Вольт";

    var inputСurrent = document.getElementById(request.serialNumber + "-inputСurrent");
    inputСurrent.textContent = request.inputСurrent + " Ампер";

    var outputСurrent = document.getElementById(request.serialNumber + "-outputСurrent");
    outputСurrent.textContent = request.outputCurrent + " Ампер";



    //var element = document.getElementById(request.serialNumber);

    ////  Make label
    //var label = document.createElement('label');
    //label.textContent = "Марка устойства: " + request.nameDevice;
    //label.setAttribure('id', 'новый_айди');
    //var br = document.createElement('br');

    //element.appendChild(br);
    //element.appendChild(label);
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});
