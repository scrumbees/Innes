
var CustomerSerach = new Object();
var _CustomorList = null;
var Mobilecount = 0;
var MailCount = 0;

app.controller('customercontroller', function ($scope, customerService, $cookies) {
    $scope.divSendMessage = true;
    $scope.format = 'yyyy-MM-dd';
    var isDefault = false;

    getAllCustomerList();
    GetTourOpCode();
    GetDeparturePoint();
    GetArrivalPoint();
    GetTravelDirection();
    GetTransportCarrier();
    GetTransportNumber();
    GetTransportType();
    GetTransportChain();
    GetCountryName();
    GetResortName();
    GetAccommodationName();

    function getAllCustomerList() {
        setData();
    }

    $scope.Search = function (Customer) {
        CustomerSerach.TourOpCode = Customer != undefined ? Customer.TourOpCode : "";
        CustomerSerach.DirectOrAgent = Customer != undefined ? Customer.DirectOrAgent : "";
        CustomerSerach.StartDate = $("#StartDate").val();
        CustomerSerach.DeparturePoint = Customer != undefined ? Customer.DeparturePoint : "";
        CustomerSerach.ArrivalPoint = Customer != undefined ? Customer.ArrivalPoint : "";
        CustomerSerach.TravelDate = $("#TravelDate").val();
        CustomerSerach.TravelDepatureTime = $("#TravelDepatureTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM")
        CustomerSerach.TravelArrivalTime = $("#TravelArrivalTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM");
        CustomerSerach.TravelDirection = Customer != undefined ? Customer.TravelDirection : "";
        CustomerSerach.TransportCarrier = Customer != undefined ? Customer.TransportCarrier : "";
        CustomerSerach.TransportNumber = Customer != undefined ? Customer.TransportNumber : "";
        CustomerSerach.TransportType = Customer != undefined ? Customer.TransportType : "";
        CustomerSerach.TransportChain = Customer != undefined ? Customer.TransportChain : "";
        CustomerSerach.CountryName = Customer != undefined ? Customer.CountryName : "";
        CustomerSerach.ResortName = Customer != undefined ? Customer.ResortName : "";
        CustomerSerach.AccommodationName = Customer != undefined ? Customer.AccommodationName : "";
        refreshDatatable();
    }

    function setData() {

        $('#CustomorListTBL').dataTable({
            "bFilter": false,
            "bInfo": true,
            "scrollX": true,
            "bServerSide": true,
            "bLengthChange": false,
            "order": [[1, "desc"]],
            "sAjaxSource": "/api/CustomerAPI/GetAllNotification/",
            "dom": 'T<"clear">lfrtip',
            tableTools: {
                "sSwfPath": "/content/swf/copy_csv_xls_pdf.swf",
                "columnDefs": [
                    { "width": "200px", "targets": 0 }
                ],
                "aButtons": [
                ]
            },
            "fnServerData": function (sSource, aoData, fnCallback) {

                aoData.push({ "name": "TourOpCode", "value": CustomerSerach.TourOpCode });
                aoData.push({ "name": "DirectOrAgent", "value": CustomerSerach.DirectOrAgent });
                aoData.push({ "name": "StartDate", "value": CustomerSerach.StartDate });
                aoData.push({ "name": "DeparturePoint", "value": CustomerSerach.DeparturePoint });
                aoData.push({ "name": "ArrivalPoint", "value": CustomerSerach.ArrivalPoint });
                aoData.push({ "name": "TravelDate", "value": CustomerSerach.TravelDate });
                aoData.push({ "name": "TravelDepatureTime", "value": CustomerSerach.TravelDepatureTime });
                aoData.push({ "name": "TravelArrivalTime", "value": CustomerSerach.TravelArrivalTime });
                aoData.push({ "name": "TravelDirection", "value": CustomerSerach.TravelDirection });
                aoData.push({ "name": "TransportCarrier", "value": CustomerSerach.TransportCarrier });
                aoData.push({ "name": "TransportNumber", "value": CustomerSerach.TransportNumber });
                aoData.push({ "name": "TransportType", "value": CustomerSerach.TransportType });
                aoData.push({ "name": "TransportChain", "value": CustomerSerach.TransportChain });
                aoData.push({ "name": "CountryName", "value": CustomerSerach.CountryName });
                aoData.push({ "name": "ResortName", "value": CustomerSerach.ResortName });
                aoData.push({ "name": "AccommodationName", "value": CustomerSerach.AccommodationName });
                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource,
                    crossDomain: true,
                    "data": aoData,
                    "success": function (json) {
                        if (json.aaData != null) {
                            Mobilecount = json.MobileCount;
                            MailCount = json.EmailCount;
                            $("#BindCount").html("<div> Total Mobile SMS Count: <span id='spanMobilecount'>" + Mobilecount + "</span></div><div> Total Email Count: " + MailCount + "</div>")
                            _CustomorList = json.aaData;
                        }
                        fnCallback(json);
                    },
                    "error": function (json) {
                    }
                });
            },
            "aoColumns": [
                {
                    "Width": "5%",
                    "sTitle": "BookingRef",
                    "mDataProp": "BookingRef",
                },
                {
                    "Width": "20px",
                    "sTitle": "TourOpCode",
                    "mDataProp": "TourOpCode",
                },
                {
                    "Width": "20px",
                    "sTitle": "PassengerId",
                    "mDataProp": "PassengerId",
                },
                {
                    "Width": "5%",
                    "sTitle": "Title",
                    "mDataProp": "Title",
                }, {
                    "Width": "5%",
                    "sTitle": "FirstName",
                    "mDataProp": "FirstName",
                },
                {
                    "Width": "5%",
                    "sTitle": "LastName",
                    "mDataProp": "LastName",
                },
                {
                    "Width": "5%",
                    "sTitle": "MobileNo",
                    "mDataProp": "MobileNo",
                },
                {
                    "Width": "10%",
                    "sTitle": "Email",
                    "mDataProp": "Email",
                }, {
                    "Width": "10px",
                    "sTitle": "DirectOrAgent",
                    "mDataProp": "DirectOrAgent",
                },
                {
                    "Width": "5%",
                    "sTitle": "StartDate",
                    "mRender": function (data, type, full) {
                        var val = DateFormatter(full.StartDate)
                        return val;
                    }
                },
                {
                    "Width": "10px",
                    "sTitle": "DeparturePoint",
                    "mDataProp": "DeparturePoint",
                },
                {
                    "Width": "10px",
                    "sTitle": "ArrivalPoint",
                    "mDataProp": "ArrivalPoint",
                },
            ]
        });

    }

    $scope.Send = function () {
        var flag = true;
        flag = ValidateMessage();
        if (!flag)
            return false;
        $("#myModal").modal()    //data-toggle="modal" data-target="#myModal"
    }

    $scope.SendMessage = function (Customer) {
        var flag = true;
        flag = ValidatePassword();
        if (!flag) {
            return flag;
        }

        CustomerSerach.TourOpCode = Customer.TourOpCode;
        CustomerSerach.DirectOrAgent = Customer.DirectOrAgent;
        CustomerSerach.StartDate = $("#StartDate").val();
        CustomerSerach.DeparturePoint = Customer.DeparturePoint;
        CustomerSerach.ArrivalPoint = Customer.ArrivalPoint;
        CustomerSerach.TravelDate = $("#TravelDate").val();
        CustomerSerach.TravelDepatureTime = $("#TravelDepatureTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM")
        CustomerSerach.TravelArrivalTime = $("#TravelArrivalTime").val().replace(" ", "").replace("AM", ":00 AM").replace("PM", ":00 PM");
        CustomerSerach.TravelDirection = Customer.TravelDirection;
        CustomerSerach.TransportCarrier = Customer.TransportCarrier;
        CustomerSerach.TransportNumber = Customer.TransportNumber;
        CustomerSerach.TransportType = Customer.TransportType;
        CustomerSerach.TransportChain = Customer.TransportChain;
        CustomerSerach.CountryName = Customer.CountryName;
        CustomerSerach.ResortName = Customer.ResortName;
        CustomerSerach.AccommodationName = Customer.AccommodationName;
        CustomerSerach.MessageText = Customer.MessageText;
        CustomerSerach.Password = Customer.Password;
        CustomerSerach.SendCountSMS = Customer.SendCountSMS;
        Customer.Mobilecount = Mobilecount;
        CustomerSerach.SendCountSMS = Customer.Mobilecount;

        var sendMessage = customerService.SendMessage(Customer);

        sendMessage.then(function (cus) {
            var res = cus.data;
            if (res == '1') {
                $scope.divSendMessage = false;
                $scope.divMessageSuccess = true;
                setTimeout(function () {
                    $scope.divMessageSuccess = false;
                }, 100)
            }
            else if (res == '2') {
                $scope.divSendMessage = true;
                $scope.divMessageSuccess = false;
                $scope.divMessageError = false;
                $scope.divMessageErrorCount = true;

                $("#divMessageErrorCount").css('opacity', '10');
                $('#divMessageErrorCount').css('display', 'block');
                window.setTimeout(function () {
                    $("#divMessageErrorCount").fadeTo(500, 0).slideUp(500, function () {
                        $(this).hide();
                    });
                }, 2000);
            }
            else if (res == '3') {
                $scope.divSendMessage = true;
                $scope.divMessageSuccess = false;
                $scope.divMessageError = true;
                $scope.divMessageErrorCount = false;

                $("#divMessageError").css('opacity', '10');
                $('#divMessageError').css('display', 'block');
                window.setTimeout(function () {
                    $("#divMessageError").fadeTo(500, 0).slideUp(500, function () {
                        $(this).hide();
                    });
                }, 2000);
            }
            else {
                $scope.divSendMessage = false;
                $scope.divMessageSuccess = true;
                setTimeout(function () {
                    $scope.divMessageSuccess = false;
                }, 100)
            }

        }, function error(data) {
        });        
    }

    function refreshDatatable() {
        $('#CustomorListTBL').dataTable().fnDraw();
    }

    $scope.close = function () {
        $scope.divSendMessage = true;
        $scope.divMessageSuccess = false;
        $scope.divMessageError = false;
        $scope.divMessageErrorCount = false;
    }

    function GetTourOpCode() {
        var promiseGet = customerService.GetTourOpCode();
        promiseGet.then(function (p1) {
            $scope.TourOpCode = p1.data
        }, function (err) {
            console.log("Error");
        });
    }

    function GetDeparturePoint() {
        var promiseGet = customerService.GetDeparturePoint();
        promiseGet.then(function (p1) { $scope.DeparturePoint = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetArrivalPoint() {
        var promiseGet = customerService.GetArrivalPoint();
        promiseGet.then(function (p1) { $scope.ArrivalPoint = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTravelDirection() {
        var promiseGet = customerService.GetTravelDirection();
        promiseGet.then(function (p1) { $scope.TravelDirection = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportCarrier() {
        var promiseGet = customerService.GetTransportCarrier();
        promiseGet.then(function (p1) { $scope.TransportCarrier = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportNumber() {
        var promiseGet = customerService.GetTransportNumber();
        promiseGet.then(function (p1) { $scope.TransportNumber = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportType() {
        var promiseGet = customerService.GetTransportType();
        promiseGet.then(function (p1) { $scope.TransportType = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetTransportChain() {
        var promiseGet = customerService.GetTransportChain();
        promiseGet.then(function (p1) { $scope.TransportChain = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetCountryName() {
        var promiseGet = customerService.GetCountryName();
        promiseGet.then(function (p1) { $scope.CountryName = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetResortName() {
        var promiseGet = customerService.GetResortName();
        promiseGet.then(function (p1) { $scope.ResortName = p1.data }, function (err) {
            console.log("Error");
        });
    }

    function GetAccommodationName() {
        var promiseGet = customerService.GetAccommodationName();
        promiseGet.then(function (p1) { $scope.AccommodationName = p1.data }, function (err) {
            console.log("Error");
        });
    }

    $scope.Clear = function (Customer) {

        Customer.TourOpCode = '';
        Customer.DirectOrAgent = '';
        Customer.StartDate = '';
        Customer.DeparturePoint = '';
        Customer.ArrivalPoint = '';
        Customer.TravelDate = '';
        Customer.TravelDepatureTime = '';
        Customer.TravelArrivalTime = '';
        Customer.TravelDirection = '';
        Customer.TransportCarrier = '';
        Customer.TransportNumber = '';
        Customer.TransportType = '';
        Customer.TransportChain = '';
        Customer.CountryName = '';
        Customer.ResortName = '';
        Customer.AccommodationName = '';
        Customer.MessageText = '';
        Customer.Password = '';
        Customer.SendCountSMS = '';
        Customer.Mobilecount = '';
        Customer.SendCountSMS = '';
    }
});

function ValidatePassword() {
    var flag = true;
    if (!ValidateRequiredField($("#txtPasswordSend"), 'Password required', 'after')) {
        flag = false;
    }
    if (!ValidateRequiredField($("#txtSendCountSMS"), 'Message count required', 'after')) {
        flag = false;
    }
    return flag;
}

function ValidateMessage() {
    var flag = true;
    if (!ValidateRequiredField($("#txtMessage"), 'Message Required', 'after')) {
        flag = false;
    }
    return flag;
}