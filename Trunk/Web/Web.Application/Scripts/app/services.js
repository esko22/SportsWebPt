define('services', ['jquery', 'config'],
    function($, config) {

        var submitSymptomDetails = function(messageKeys, onSuccess, onError, context) {
            var options = {
                url: "/examine/diffdiag",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                type: "POST",
                data: JSON.stringify(messageKeys),
                cache: false,
                success: function(result) {
                    if (onSuccess) {
                        onSuccess(result);
                    }
                },
                error: function(xml, status, e) {
                    if (onError) {
                        onError(xml, status, e);
                    }
                }
            };

            $.ajax(options);
        },
        addToFavorites = function (messageKeys, onSuccess, onError, context) {
            var options = {
                url: "/users/favorites",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                type: "POST",
                data: JSON.stringify(messageKeys),
                cache: false,
                success: function (result) {
                    if (onSuccess) {
                        onSuccess(result);
                    }
                },
                error: function (xml, status, e) {
                    if (onError) {
                        onError(xml, status, e);
                    }
                }
            };

            $.ajax(options);
        },
            getInjuryDetail = function (searchKey, onSuccess, onError, context) {
                var options = {
                    url: $.format("{0}{1}", config.apiUris.injuryDetail, searchKey),
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                type: "GET",
                cache: false,
                success: function (result) {
                    if (onSuccess) {
                        onSuccess(result);
                    }
                },
                error: function (xml, status, e) {
                    if (onError) {
                        onError(xml, status, e);
                    }
                }
            };

            $.ajax(options);
        };

        return {
            submitSymptomDetails: submitSymptomDetails,
            addToFavorites: addToFavorites,
            getInjuryDetail: getInjuryDetail
        };

    });