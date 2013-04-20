define('vm.examine.container',
    ['ko'],
    function (ko) {

        var selectedAreas = ko.observableArray();

        return {
            selectedAreas: selectedAreas
        };
    });