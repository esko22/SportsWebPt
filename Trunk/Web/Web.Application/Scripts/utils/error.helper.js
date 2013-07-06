define('error.helper', ['jquery'], function($) {

    var onError = function(message) {
        alert($.format('{0} -- {1}', message, 'something fucked up'));
    };

    return {
        onError: onError
    };

});
