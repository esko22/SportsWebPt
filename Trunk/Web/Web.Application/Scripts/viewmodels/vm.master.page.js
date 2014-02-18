define('vm.master.page', ['vm.splash', 'vm.examine.page', 'vm.research.page', 'vm.dashboard.page', 'config'],
    function (splashPage, examinePage, researchPage, dashboardPage,config) {

        var player;

        function youtubeVideoStop(ytId) {

            if (typeof player !== "undefined") {
                player.stopVideo();
            }

            player = new YT.Player('yt-' + ytId);
        }

        return {
            splashPage: splashPage,
            examinePage: examinePage,
            researchPage: researchPage,
            dashboardPage: dashboardPage,
            templateIds: config.templateIds,
            youtubeVideoStop: youtubeVideoStop
        };
    });