define('vm.admin.injuries',
    ['ko', 'underscore', 'knockback', 'model.admin.injury.collection'],
    function (ko, _, kb, InjuryCollection) {

        var injuryListTemplate = 'admin.injury.list',
            injuryCollection = new InjuryCollection(),
            injuries = kb.collectionObservable(injuryCollection),
            editInjury = function (data, event) {
            },
            addInjury = function (data, event) {
            },
            bindSelectedInjury = function (data, event) {
            },
        onSuccessfulChangeCallback = function () {
            injuryCollection.fetch();
        };

        injuryCollection.fetch();

        return {
            injuries: injuries,
            injuryListTemplate: injuryListTemplate,
            bindSelectedInjury: bindSelectedInjury,
            onSuccessfulChangeCallback: onSuccessfulChangeCallback,
            addInjury: addInjury,
            editInjury : editInjury
        };
    });