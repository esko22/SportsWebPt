define('vm.examine.container',
    ['ko'],
    function (ko) {

        var selectedHotspots = ko.observableArray();

        return {
            selectedHotspots: selectedHotspots
        };
    });