define('vm.contact.page', ['knockback', 'ko'],
    function (kb, ko) {

        var isVisible = ko.observable(false);

        function onVisible() {
            isVisible(true);
        }

        return {
            isVisible: isVisible,
            onVisible: onVisible,
        };
    });