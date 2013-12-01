define('vm.share.bar',
    ['ko', 'config', 'jquery','favorites.helper'],
    function (ko, config, $, favHelper) {

        function ShareBar() {
            this.shareUri = ko.observable();
            this.entityType = '';
            this.entityId = '';
        }


        ShareBar.prototype.addAsFavorite = function (data, event) {
            alert(this.entityType + this.entityId);
                favHelper.addEntityToFavorites(this.entityType, this.entityId);
            };


        ShareBar.prototype.init = function(pageUri, favEntityType, favEntityId) {
            this.entityType = favEntityType,
            this.entityId = favEntityId,
            this.shareUri(pageUri);
        };

        return ShareBar;
    });