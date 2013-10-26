define('model.user', ['backbone', 'config', 'model.favorite', 'model.favorite.collection'],
    function (backbone, config, Favorite, FavoriteCollection) {
       var
           user = backbone.RelationalModel.extend({
               urlRoot: config.apiUris.users,
               defaults: {
                   'emailAddress': '',
               },
               relations: [{
                   type: backbone.HasMany,
                   key: 'injuries',
                   relatedModel: Favorite,
                   collectionType: FavoriteCollection
               },
                {
                    type: backbone.HasMany,
                    key: 'exercises',
                    relatedModel: Favorite
                },
                {
                    type: backbone.HasMany,
                    key: 'plans',
                    relatedModel: Favorite
                },
                {
                    type: backbone.HasMany,
                    key: 'videos',
                    relatedModel: Favorite
               }]
        });

       return user;

    });