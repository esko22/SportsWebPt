define('vm.admin.injury.page',
    ['ko', 'underscore', 'knockback', 'jquery', 'vm.admin.injury.form', 'vm.admin.injuries'],
    function (ko, _, kb, $, form, injuries) {


        var bindViewModels = function () {
            injuries.bindSelectedInjury = form.bindSelectedInjury;
            form.suscribe(injuries.onSuccessfulChangeCallback);
            kb.applyBindings(form, $('#admin-injury-form').get(0));
            kb.applyBindings(injuries, $('#admin-injury-list').get(0));
        };

        return {
            bindViewModels: bindViewModels,
        };

    })