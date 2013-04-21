define('vm.examine.description.component',
    ['ko'],
    function (ko) {

        var selectedAreas = ko.observableArray();

        return {
            selectedAreas: selectedAreas
        };
    });