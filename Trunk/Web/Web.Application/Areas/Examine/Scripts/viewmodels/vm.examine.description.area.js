define('vm.examine.description.area',
    ['ko'],
    function (ko) {

        var areaComponents = ko.observableArray();

        return {
            areaComponents: areaComponents
        };
    });