
var CustomerSerach = new Object();
var _CustomorList = null;
var Mobilecount = 0;
var MailCount = 0;
app.controller('customercontroller', function ($scope, customerService, $cookies) {
    $scope.divSendMessage = true;

    $scope.format = 'yyyy-MM-dd';
    getAllCustomerList();
    var isDefault = false;

    function getAllCustomerList() {
        setData();
    }

    $scope.Search = function (Customer) {

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
                            $("#BindCount").html("<div> Total Mobile Count:" + Mobilecount + "</div><div> Total Email Count:" + MailCount + "</div>")
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

        var sendMessage = customerService.SendMessage(Customer);
        sendMessage.then(function success(data) {

            $scope.divSendMessage = false;
            $scope.divMessageSuccess = true;
            setTimeout(function () {
                $scope.divMessageSuccess = false;
            }, 100)

        }, function error(data) {

            //alert(JSON.stringify(data));

        });
    }

    function refreshDatatable() {
        $('#CustomorListTBL').dataTable().fnDraw();
    }

    $scope.close = function () {
        $scope.divSendMessage = true;
        $scope.divMessageSuccess = false;
    }

});


function ValidatePassword() {
    var flag = true;
    if (!ValidateRequiredField($("#txtPasswordSend"), 'Password Required', 'after')) {
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