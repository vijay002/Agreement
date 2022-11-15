
var grid_selector = '#tblAgreementDataGrid';


function LoadGrid()
{
    $(grid_selector).DataTable({
        "bProcessing": true,
        "bServerSide": true,
        "lengthMenu": [
            [5, 10, 25, 50, 100],
            [5, 10, 25, 50, 100]
        ],
        "sAjaxSource": "/home/LoadData",
        "fnServerData": function (sSource, aoData, fnCallback) {
            //aoData.push({ "name": "more_data", "value": "my_value" });
            //$.getJSON(sSource, aoData, function (json) {
            //    /* Do whatever additional processing you want on the callback, then tell DataTables */
            //    fnCallback(json)
            //});
            $.ajax({
                'dataType': 'json',
                'type': 'POST',
                'url': sSource,
                'data': aoData,
                'success': function (data) {
                    fnCallback(data);
                },
            });



        },
        "aoColumns": [
            {
                "sName": "Id",
                "bSearchable": false,
                "bSortable": false,
                "bVisible": false

            },
            { "sName": "ProductId" },
            { "sName": "ProductGroupId" },
            {
                "sName": "EffectiveDate",
                "sClass": "center",
                "bSortable": true

            },
            {
                "sName": "ExpirationDate",
                "sClass": "center",
                "bSortable": true
            },
            {
                "sName": "ProductPrice",
                "sClass": "center",
                "bSortable": true
            },
            {
                "sName": "NewPrice",
                "sClass": "center",
                "bSortable": true
            },
            {
                "bSortable": false,
                "sClass": "center",
                "sWidth": "10%",
                "render": function (data, type, row, meta) {
                    var buttons = "";
                    buttons = '<center>';
                    buttons += '&nbsp;<i  data-modal="" id="btnDelete" name="btnAction" style="cursor:pointer;" Title="Delete Record" class="fa fa-trash-o" onClick="DeleteRecord(' + row[0] + ')"></i></center>';
                    return buttons;
                }
            }
        ]
    });


}


function DeleteRecord(id) {
    if (confirm("Confim to delete ?")) {

        $.post("/agreement/delete?id=" + id, function (data, status) {
            if (data.success) {
                $(grid_selector).DataTable().ajax.reload();
            } else {

            }
        });
    }
}

function AddEditAgreement(id) {

    $.ajax({
        type: "GET",
        contentType: 'application/json',
        url: '/Home/Edit',
        data: { 'id': id },
        success: function (data) {
            bootbox.dialog(
                {
                    message: data,
                    title: 'Manage Agreement',
                    className: "modal-darkorange",
                    buttons: {
                        success: {
                            label: "Save",
                            className: 'btn btn-primary btn-md',
                            callback: function () {
                                SaveAgreement();
                            }
                        },
                        "Cancel": {
                            className: 'btn btn-outline-secondary btn-md', //'btn btn-outline-secondary btn-md'
                            callback: function () {

                            }
                        }
                    }
                });
        }
        
            
        
    }).done(function () {
        AutoCompleteProductGroup();
        AutoCompleteProduct();

        //$(".clsInputdate").datepicker({
        //    format: "dd-M-yyyy",
        //    todayHighlight: true,
        //    autoclose: true,
        //    clearBtn: true
        //});

    });
}

function SaveAgreement() {
    if ($("#frmAgreement").validate()) {
        var form_data = $("#frmAgreement").serialize();
        $.post('/home/EditAgreementSave', form_data, function (data) {
            if (data.success) {//on success refresh view
                $(grid_selector).DataTable().ajax.reload();
            }
            else {
                //ErrorMessage(data.message);
            }
        });
    }
}

function AutoCompleteProduct() {

    $("#txtProduct").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Product/GetProductList',
                dataType: "json",
                type: 'GET',
                data: { query: request.term },
                success: function (data) {
                    //var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    //response($.grep(data, function (value) {
                    //    return matcher.test(value.text);
                    //}));

                    console.log(data);
                    //response(data);

                    response($.map(data.data, function (item) {
                        return { value: item.id, label: item.productDescription }
                    }));

                    //console.log(item);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //$.loader.close();
                    $.toastr.error(textStatus);
                }
            });
        },
        minLength: 1,
        autoSelect: true,
        select: function (event, ui) {
            $('#txtProduct').val(ui.item.label);
            $('#hdnProductId').val(ui.item.value);
            event.preventDefault();
        },
        focus: function (event, ui) {
            $("#txtProduct").val(ui.item.label);

            event.preventDefault();
        },
        change: function (event, ui) {
            if (ui.item) {
                // do whatever you want to when the item is found

            }
            else {
                $("#hdnProductId").val("");
                $("#txtProduct").val("");
            }
        },
        response: function (event, ui) {
            if (ui.content.length === 0) {
                //No results found
                $('#hdnProductId').val('');
                $('#txtProduct').val('');
            }
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
    
}


function AutoCompleteProductGroup() {

    $("#txtProductGroup").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/ProductGroup/GetProductGroupList',
                headers: {
                    "RequestVerificationToken":
                        $('input[name="__RequestVerificationToken"]').val()
                },
                dataType: "json",
                type: 'GET',
                data: { query: request.term },
                success: function (data) {
                    //var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    //response($.grep(data, function (value) {
                    //    return matcher.test(value.text);
                    //}));

                    console.log(data);
                    //response(data);

                    response($.map(data.data, function (item) {
                        return { value: item.id, label: item.groupDescription }
                    }));

                    //console.log(item);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //$.loader.close();

                }
            });
        },
        minLength: 1,
        autoSelect: true,
        select: function (event, ui) {
            $('#txtProductGroup').val(ui.item.label);
            $('#hdnProductGroupId').val(ui.item.value);
            event.preventDefault();
        },
        focus: function (event, ui) {
            $("#txtProductGroup").val(ui.item.label);

            event.preventDefault();
        },
        change: function (event, ui) {
            if (ui.item) {
                // do whatever you want to when the item is found

            }
            else {
                $("#hdnProductGroupId").val("");
                $("#txtProductGroup").val("");
            }
        },
        response: function (event, ui) {
            if (ui.content.length === 0) {
                $('#hdnProductGroupId').val('');
                $('#txtProductGroup').val('');
            }
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }

    });
    //.autocomplete("instance")._renderItem = function (ul, item) {
    //    return $("<li></li>")
    //        .append("<a style><div class='details-name'>" + item.label + "</div><div style='color:gray;margin-left:10px;'>"
    //                +"</div>" + "</a>")
    //        .appendTo(ul);

    //};

}