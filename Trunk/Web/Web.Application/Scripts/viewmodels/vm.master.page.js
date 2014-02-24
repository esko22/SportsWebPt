define('vm.master.page', ['vm.splash', 'vm.examine.page', 'vm.research.page', 'vm.dashboard.page', 'config', 'vm.equipment.list.dialog'],
    function (splashPage, examinePage, researchPage, dashboardPage,config, equipmentPage) {

        var player,
        equipmentList = equipmentPage;

        function youtubeVideoStop(ytId) {

            if (typeof player !== "undefined") {
                player.stopVideo();
            }

            player = new YT.Player('yt-' + ytId);
        }

        function showEquipmentList(data, equipmentCollection) {
            equipmentList.init(data);
            $('#equipment-list-dialog').modal('show');
        }




        return {
            splashPage: splashPage,
            examinePage: examinePage,
            researchPage: researchPage,
            dashboardPage: dashboardPage,
            equipmentPage: equipmentPage,
            templateIds: config.templateIds,
            youtubeVideoStop: youtubeVideoStop,
            showEquipmentList: showEquipmentList
        };
    });