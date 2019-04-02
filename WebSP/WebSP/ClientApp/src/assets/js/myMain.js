jQuery(document).ready(function() {
    var productDetails = 'div.item.prod-select';
    //text2html();
    jQuery(productDetails).hide();
    jQuery(`
        .adminWindows, 
        .mailContent, 
        #newProductForm, 
        #editProductForm, 
        #newServiceForm, 
        #editServiceForm,
        #home_DetailsForm,
        #homeSlideForm`).hide();
    jQuery('#productWindow, #serviceList').slideDown();

    /**
     * Products (main page)
     */
    jQuery("li.p-selector").click(function() {

        jQuery(productDetails).fadeOut(50);
        jQuery(productDetails).removeClass('white-bg');

        var _index = jQuery(this).index();

        if (_index === jQuery(productDetails).length) {
            jQuery(productDetails).hide();
            jQuery('.to-hide').show();
        } else {
            jQuery(productDetails).eq(_index).addClass('white-bg ui segment');
            jQuery(productDetails).eq(_index).fadeIn(1000);
            jQuery('.to-hide').hide();
        }
    });


    /**
     * Products (admin page)
     */
    jQuery('.newProduct').click(function() {
        jQuery('#productList').hide();

        jQuery('#newProductForm').slideDown();
    });

    jQuery('#cancelProduct, #cancelEditProduct').click(function() {
        jQuery('#productList').slideDown();

        jQuery('#newProductForm, #editProductForm').hide();
        //jQuery('#editProductForm').hide();
    });

    /**
     * Services (admin page)
     */
    jQuery('.newService').click(function() {
        jQuery('#serviceList').hide();

        jQuery('#newServiceForm').slideDown();
    });

    jQuery('#cancelService, #cancelEditService').click(function() {
        jQuery('#serviceList').slideDown();

        jQuery('#newServiceForm, #editServiceForm').hide();
        //jQuery('').hide();
    });

    jQuery('.newSlide').click(function() {
        jQuery('#SlideImage').slideDown();
        jQuery('#slidesList').hide();

        jQuery('#homeSlideForm').slideDown();
    });
    jQuery('.editHomePage').click(function() {
        jQuery('#slidesList').hide();

        jQuery('#home_DetailsForm').slideDown();
    });
    jQuery('#cancelhomeSlide, #homeDetailsCancel').click(function() {
        jQuery('#slidesList').slideDown();
        jQuery('#slideForm').attr('action', '/homeSlide');
        jQuery('#SlideName').val('');
        jQuery('#SlideDescription').val('');
        jQuery('#homeSlideForm, #home_DetailsForm').hide();
        //jQuery('').hide();
    });



    /**
     * Delete product
     */
    jQuery('button.deleteProduct').click(function() {
        console.log('delete clicked: ' + '/product/' + jQuery(this).attr("p-id"));
        event.stopPropagation();
        jQuery.ajax({
            url: '/product/' + jQuery(this).attr("p-id"),
            method: 'DELETE',
            success: function(data) {
                location.reload(true);
            }
        });
    });

    /**
     * Delete service
     */
    jQuery('button.deleteService').click(function() {
        console.log('delete clicked: ' + '/service/' + jQuery(this).attr("s-id"));
        event.stopPropagation();
        jQuery.ajax({
            url: '/service/' + jQuery(this).attr("s-id"),
            method: 'DELETE',
            success: function(data) {
                location.reload(true);
            }
        });
    });

    /**
     * Delete Home Slide
     */
    jQuery('button.deleteSlide').click(function() {
        alert("button.deleteSlide");
        event.stopPropagation();
        jQuery.ajax({
            url: '/homeSlide/' + jQuery(this).attr("hs-id"),
            method: 'DELETE',
            success: function(data) {
                location.reload(true);
                //getHomeSlides();
            }
        });
    });

    /**
     * Get product
     */
    jQuery('button.editProduct').click(function() {
        event.stopPropagation();
        jQuery.ajax({
            url: '/product/' + jQuery(this).attr("p-id"),
            method: 'GET',
            success: function(data) {
                jQuery('#updateForm').attr('action', '/product/' + data._id + '?_method=PUT');
                jQuery('#update-prod_name').val(data.productName);
                jQuery('#update-prod_price').val(data.price);
                jQuery('#update-prod_description').val(data.description);
                jQuery('#update-prod_family').val(data.family);
                jQuery('#update-prod_image').val(data.image);
                jQuery('#productList').hide();
                jQuery('#newProductForm').hide();
                jQuery('#editProductForm').slideDown();
            }
        });
    });

    /**
     * Get service
     */
    jQuery('button.editService').click(function() {
        event.stopPropagation();
        jQuery.ajax({
            url: '/service/' + jQuery(this).attr("s-id"),
            method: 'GET',
            success: function(data) {
                jQuery('#serviceUpdateForm').attr('action', '/service/' + data._id + '?_method=PUT');
                jQuery('#update-serv_name').val(data.name);
                jQuery('#update-serv_icon').val(data.icon);
                jQuery('#update-serv_description').val(data.description);
                jQuery('#serviceList').hide();
                jQuery('#newServiceForm').hide();
                jQuery('#editServiceForm').slideDown();
            }
        });
    });

    /**
     * Get Home Slide
     */
    jQuery('button.editSlide').click(function() {
        event.stopPropagation();
        jQuery.ajax({
            url: '/homeSlide/' + jQuery(this).attr("hs-id"),
            method: 'GET',
            success: function(data) {
                jQuery('#slideForm').attr('action', '/homeSlide/' + data._id + '?_method=PUT');
                jQuery('#slideForm').removeAttr("enctype");
                jQuery('#SlideName').val(data.name);
                jQuery('#SlideImage').val(data.image);
                jQuery('#SlideDescription').val(data.description);
                jQuery('#slidesList').hide();
                jQuery('#home_DetailsForm').hide();
                jQuery('#homeSlideForm').slideDown();
            }
        });
    });


    /**
     * admin options selectors
     */
    jQuery('.viewSelector').click(function() {
        //console.log(jQuery(this).index());
        if (jQuery(this).index() === 0) {
            getHomeDetails();
            //getHomeSlides();
        }
        jQuery('.adminWindows, .mailContent').hide();
        jQuery('.viewSelector').removeClass('selected_');
        jQuery(this).addClass('selected_')
        var opID = '#' + jQuery(this).attr('divSelector');
        jQuery(opID).slideDown();

    });

    function getHomeDetails() {
        jQuery.ajax({
            url: '/homeDetails/list',
            method: 'GET',
            success: function(data) {
                jQuery('#homeDetailsForm').attr('action', '/homeDetails/' + data[0]._id + '?_method=PUT');
                jQuery('#aboutUs').val(data[0].aboutUs);
                jQuery('#mission').val(data[0].mission);
                jQuery('#vision').val(data[0].vision);
                jQuery('#coreValues').val(data[0].coreValues);
                jQuery('#streetNo').val(data[0].streetNo);
                jQuery('#area').val(data[0].area);
                jQuery('#town').val(data[0].town);
                jQuery('#zipCode').val(data[0].zipCode);
                jQuery('#inputEmail').val(data[0].emails);
                jQuery('#inputPhone').val(data[0].phones);
            }
        });
    }

    /*function getHomeSlides() {
        jQuery.ajax({
            url: '/homeSlide/list',
            method: 'GET',
            success: function(data) {
                jQuery('#homeSlides').empty();
                var markup;
                for (var index = 0; index < data.length; index++) {
                    var element = data[index];
                    jQuery('#homeSlides').append(
                        '<div class="list-group-item">' +
                        '<img class="ui mini image" src=' + element.image + '>' +
                        '<h4 class="list-group-item-heading">' + element.name + '</h4>' +
                        '<p class="list-group-item-text">' + element.description + '</p>' +
                        '<div class="btn-group">' +
                        '<button hs-id=' + element._id + ' class="btn-default editSlide">' +
                        '<i class="fa fa-pencil-square-o"> EDIT</i>' +
                        '</button>' +
                        '<button hs-id=' + element._id + ' class="btn-danger deleteSlide">' +
                        '<i class="fa fa-trash-o fa-x3"> DELETE</i>' +
                        '</button>' +
                        '</div>' +
                        '</div>'
                    );
                    markup += 
                }
                jQuery('#homeSlides').innerHTML = markup;
            }
        });
    }*/

    /**
     * Messages not read
     */
    jQuery('#msgCount').text(jQuery('td>i.fa-envelope').length);

    /**
     * text to HTML
     */
    function text2html_() {
        var selector = 'div.description p, .placeIcon, .icon-holder, .htmlText';
        for (var indx = 0; indx < jQuery(selector).length; indx++) {
            var txt = jQuery(selector)[indx].textContent;
            //console.log(txt);
            jQuery(selector)[indx].textContent = '';
            jQuery(selector)[indx].innerHTML = txt;
        }
    }
    text2html_();


    // fix main menu to page on passing
    jQuery('.main.menu').visibility({
        type: 'fixed'
    });

    // lazy load images
    jQuery('.image').visibility({
        type: 'image',
        transition: 'vertical flip in',
        duration: 500
    });

    // jQuery('.selectpicker').selectpicker({
    //     style: 'btn-info',
    //     size: 4
    // });


});