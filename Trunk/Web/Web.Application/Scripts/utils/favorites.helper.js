define('favorites.helper', ['jquery','services', 'error.helper', 'config'], function ($, services, err, config) {

    var addEntityToFavorites = function(entity, id) {

        

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