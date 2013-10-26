define('model.favorite', ['backbone','config'],
    function (backbone, config) {
    	var
			favorite = backbone.RelationalModel.extend({
				urlRoot: config.apiUris.favorites,
				defaults: {
				    'entityName': '',
				    'entity': '',
				    
				}
			});

    	return favorite;

    });

define('model.favorite.collection', ['backbone', 'model.favorite', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.favorites
            });

        return componentCollection;

    });