define('favorites.helper', ['jquery','services', 'error.helper', 'config'], function ($, services, err, config) {

    var addEntityToFavorites = function(entity, id) {

        //TODO: need to create a user object
        if ($('#userid').val() === '0')
            $('#login-dialog').modal('toggle');
        
        var messageKeys = {
            'entity': entity,
            'entityId': id
        };

        services.addToFavorites(messageKeys, null, err.onError);
    };

    return {
        addEntityToFavorites: addEntityToFavorites
    };


});