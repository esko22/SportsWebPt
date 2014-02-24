define('vm.research.nav.bar',
    ['ko', 'config', 'jquery','favorites.helper'],
    function (ko, config, $, favHelper) {

        function ResearchNavBar() {
            this.shareUri = ko.observable();
            this.entityType = ko.observable();
            this.entityId = '';
            this.returnUri = ko.observable();
        }


        ResearchNavBar.prototype.addAsFavorite = function (data, event) {
            alert(this.entityType() + this.entityId);
                favHelper.addEntityToFavorites(this.entityType(), this.entityId);
            };


        ResearchNavBar.prototype.init = function (pageUri, favEntityType, favEntityId, returnUri) {
            this.entityType(favEntityType),
            this.entityId = favEntityId,
            this.shareUri(pageUri);
            this.returnUri(returnUri);
        };

        return ResearchNavBar;
    });