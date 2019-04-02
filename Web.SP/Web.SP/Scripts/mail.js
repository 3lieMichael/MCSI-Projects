jQuery(document).ready(function() {
    /**
     * Mails (admin page)
     */
    jQuery('tr').click(function() {
        var idNumber = jQuery(this).attr("id").split("-")[1];
        jQuery('#mail-' + idNumber).slideDown();
        openedMail(idNumber);
    });
    /**
     * Read message
     */
    function openedMail(mailIndex) {
        var thisIndex = mailIndex; //jQuery(this).attr("_index");
        btn = jQuery('#btn-' + thisIndex);
        var message = {
            opened: btn.attr("opened")
        };
        jQuery.ajax({
            //dataType: 'json',
            url: '/message/' + btn.attr("msgDBid"),
            data: message,
            method: 'PUT',
            success: function(data) {
                console.log(data);
                if (jQuery('td>i.fa-envelope').length > 0) {
                    jQuery('#msgCount').text(jQuery('td>i.fa-envelope').length - 1);
                }
                //jQuery('#mail-' + thisIndex).hide();
                jQuery('#mailRow-' + thisIndex).removeClass('notRead');
                jQuery('#mailIconR-' + thisIndex).removeClass('fa-envelope');
                jQuery('#mailIconR-' + thisIndex).addClass('fa-envelope-open-o');
            }
        });
    }
    /**
     * Delete mail
     */
    jQuery('.deleteMail').click(function() {
        event.stopPropagation();
        var thisIndex = jQuery(this).attr("id").split("-")[1]; //jQuery(this).attr("_index");
        btn = jQuery('#btnDelete-' + thisIndex);
        jQuery.ajax({
            //dataType: 'json',
            url: '/message/' + btn.attr("msgDBid"),
            method: 'DELETE',
            success: function(data) {
                // if (jQuery('td>i.fa-envelope').length > 0) {
                //     jQuery('#msgCount').text(jQuery('td>i.fa-envelope').length - 1);
                // }
                jQuery('#mail-' + thisIndex).remove();
                jQuery('#mailRow-' + thisIndex).remove();
            }
        });
    });

    /**
     * Check for new email on the server
     */
    function newMailCheck() {
        //console.log('new mail checker fired: ' + jQuery('table#msgTable tbody tr').length / 2);
        jQuery.ajax({
            //dataType: 'json',
            url: '/message/api',
            method: 'GET',
            success: function(data) {
                if (data.length > (jQuery('table#msgTable tbody tr').length / 2)) {
                    console.log('new mail found');
                    // bootbox.confirm({
                    //     message: "new mail found",
                    //     buttons: {
                    //         confirm: {
                    //             label: 'Yes',
                    //             className: 'btn-success'
                    //         },
                    //         cancel: {
                    //             label: 'No',
                    //             className: 'btn-danger'
                    //         }
                    //     },
                    //     callback: function(result) {
                    //         console.log('This was logged in the callback: ' + result);
                    //     }
                    // });
                    var n = jQuery('table#msgTable tbody tr').length / 2;
                    var l = data.length;
                    for (var i = n; i < l; i++) {
                        var element = data[i];
                        var markup = "<tr id='mailRow-" + (i + 1) + "' class='notRead'>" +
                            "<td>*</td>" +
                            "<td>" + element.name + "</td>" +
                            "<td class='mailHead' id='mailHead-" + (i + 1) + "'>" + element.title + "</td>" +
                            "<td>" + element.email + "</td>" +
                            "<td><i id='mailIconR-" + (i + 1) + "' class='fa fa-envelope' aria-hidden='true'></i></td>" +
                            "<td><button id='btnDelete-" + (i + 1) + "' msgDBid='" + element._id + "' class='btn-link deleteMail'>" +
                            "<i class='fa fa-trash' aria-hidden='true'></i>" +
                            "</button>" +
                            "</td>" +
                            "</tr>" +
                            "<tr id='mail-" + (i + 1) + "' class='mailContent'>" +
                            "<td colspan='6'>" +
                            "<div class='ui segment'>" +
                            "<em>Sender: </em>" + element.name + "<br>" +
                            "<em>Subject: </em>" + element.title + "<br>" +
                            "<em>Email: </em>" + element.email + "<br>" +
                            "<em>Contact Number: </em>" + element.phone + "<hr class='contentSplitter'>" +
                            element.text +
                            "</div>" +
                            "<button id='btn-" + (i + 1) + "' opened='yes' msgDBid='" + element._id + "' class='btn-link closeMail'>Close Message</button>" +
                            "</td>" +
                            "</tr>";
                        jQuery("table#msgTable tbody").prepend(markup);
                        jQuery('#mail-' + (i + 1)).hide();
                        var mailsCount = parseInt(jQuery('#msgCount').text()) + l - n;
                        //console.log('mailsCount = ' + jQuery('#msgCount').text() + ' + ' + l + ' - ' + n);
                        jQuery('#msgCount').text(mailsCount);
                    }
                }
            }
        });
    }
    setInterval(newMailCheck, 60000);

    /**
     * Close Mail
     */
    jQuery('.closeMail').click(function() {
        event.stopPropagation();
        var index = jQuery(this).attr('id').split('-')[1];
        jQuery('#mail-' + index).hide();
        console.log('#mail-' + index);
    });
});