
var grid_selector = '#tblAgreementDataGrid';


function LoadGrid()
{
    $(grid_selector).DataTable({
        "bProcessing": true,
        "bServerSide": true,
        "iDisplayLength": 10,
        "sPaginationType": "numbers",
        "lengthMenu": [
            [5,10, 25, 50, 100],
            [5, 10, 25, 50, 100]
        ],
        "sAjaxSource": "/home/LoadData",
        "fnServerData": function (sSource, aoData, fnCallback) {
            
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
            { "sName": "Product" },
            { "sName": "ProductGroup" },
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
               

                "mRender": function (data, type, full) {
                    return '<a class="btn btn-info btn-sm" href=#/' + full[0] + ' onclick = "AddEditAgreement(' + full[0]  + ');">' + 'Edit' + '</a>'
                        + '&nbsp;<a class="btn btn-danger btn-sm" href=#/' + full[0] + ' onclick="DeleteRecord(' + full[0]  + ');" >' + 'Delete' + '</a>';
                }
            }

           


        ]
    });


}


function DeleteRecord(id) {
    if (confirm("Confim to delete ?")) {

        $.post("/home/delete?id=" + id, function (data, status) {
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
                                var formIsValid = $("#frmAgreement").valid();
                                if (!formIsValid)
                                    return false;
                                else 
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
    else {
        return false;
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