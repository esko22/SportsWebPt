define('config.lookups', ['ko', 'jquery', 'knockback','model.sign.filter.collection'],
    function (ko, $, kb, SignFilters) {

        var signFilterCollection = new SignFilters(),
            availableSignFilters = kb.collectionObservable(signFilterCollection);

        function init() {
            signFilterCollection.fetch();
        }

        init();

        return {
            availableSignFilters: availableSignFilters
        };
    });
