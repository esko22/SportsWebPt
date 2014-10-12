'use strict';


var swptApp = angular.module('swptApp', ['ngResource', 'ui.router', 'ngAnimate','ngTouch', 'jquery.plugin.module', 'shared.ui', 'examine',
    'research', 'ui.bootstrap', 'ngSanitize', 'kendo.directives', 'patient.module', 'util.module', 'common.filters', 'angular-google-analytics', 'about.module', 'admin.module',
    'ngGrid', 'therapist.module', 'clinic.module', 'registration.module'])
    .config(['$urlRouterProvider', '$stateProvider', '$httpProvider', '$provide', 'AnalyticsProvider', '$locationProvider',
        function ($urlRouterProvider, $stateProvider, $httpProvider, $provide, AnalyticsProvider, $locationProvider) {

        AnalyticsProvider.setAccount('UA-39296197-3');
        AnalyticsProvider.trackPages(true);
        AnalyticsProvider.setPageEvent('$stateChangeSuccess');
        AnalyticsProvider.useAnalytics(true);

        //Optional set domain (Use 'none' for testing on localhost)
        //AnalyticsProvider.setDomainName('none');

            $stateProvider
                .state('admin',
                {
                    abstract: true,
                    views: {
                        "core-view": { templateUrl: '/app/prtl.core.htm' }
                    },
                    data: {
                        access: 'access.admin'
                    }
                })
                .state('admin.dashboard',
                {
                    url: "/admin",
                    views: {
                        "core-app-view": { templateUrl: '/app/admin/prtl.admin.dashboard.htm' }
                    }
                })
                .state('admin.dashboard.bodyregion',
                {
                    url: "/bodyregion",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.body.region.htm',
                            controller: 'BodyRegionController'
                        }
                    }
                })
                .state('admin.dashboard.bodypart',
                {
                    url: "/bodypart",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.body.part.htm',
                            controller: 'BodyPartController'
                        }
                    }
                })
                .state('admin.dashboard.prognosis',
                {
                    url: "/prognosis",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.prognosis.htm',
                            controller: 'PrognosisController'
                        }
                    }
                })
                .state('admin.dashboard.treatment',
                {
                    url: "/treatment",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.treatment.htm',
                            controller: 'TreatmentController'
                        }
                    }
                })
                .state('admin.dashboard.equipment',
                {
                    url: "/equipment",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.equipment.htm',
                            controller: 'EquipmentController'
                        }
                    }
                })
                .state('admin.dashboard.cause',
                {
                    url: "/cause",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.cause.htm',
                            controller: 'CauseController'
                        }
                    }
                })
                .state('admin.dashboard.sign',
                {
                    url: "/sign",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.sign.htm',
                            controller: 'SignController'
                        }
                    }
                })
                .state('admin.dashboard.video',
                {
                    url: "/video",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/entities/prtl.video.htm',
                            controller: 'VideoController'
                        }
                    }
                })
                .state('admin.dashboard.exercise',
                {
                    url: "/exercise",
                    views: {
                        "admin-content-view": {
                            templateUrl: '/app/admin/exercises/prtl.exercise.publish.htm',
                            controller: 'ExerciseAdminController'
                        }
                    }
                })
                .state('admin.dashboard.plan',
                {
                    url: "/plan",
                    views: {
                        "admin-content-view": {
                            templateUrl: '/app/admin/plans/prtl.plan.publish.htm',
                            controller: 'PlanAdminController'
                        }
                    }
                })
                .state('admin.dashboard.pubinjury',
                {
                    url: "/injury/publish",
                    views: {
                        "admin-content-view": {
                            templateUrl: '/app/admin/injuries/prtl.injury.publish.htm',
                            controller: 'InjuryPublishController'
                        }
                    }
                })
                .state('admin.dashboard.injury',
                {
                    url: "/injury",
                    views: {
                        "admin-entity-view": {
                            templateUrl: '/app/admin/injuries/prtl.injury.htm',
                            controller: 'InjuryAdminController'
                        }
                    }
                })
                .state('public',
                {
                    abstract: true,
                    views: {
                        "core-view": { templateUrl: '/app/prtl.core.htm' }
                    },
                    data: {
                        access: 'access.anon'
                    }
                })
                .state('public.notfound',
                {
                    url: "/404",
                    views: {
                        "core-app-view": { templateUrl: '/app/shared/prtl.not.found.htm' }
                    },
                    data: {
                        access: 'access.anon'
                    }
                })
                .state('public.splash',
                {
                    url: "/",
                    views: {
                        "core-app-view": { templateUrl: '/app/splash/prtl.splash.htm' }
                    }
                })
                .state('public.sitemap',
                {
                    url: "/sitemap",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/about/prtl.site.map.htm',
                            controller: 'SiteMapController'
                        }
                    }
                })
                .state('public.about',
                {
                    url: "/about/:aboutType",
                    views: {
                        "core-app-view": { templateUrl: '/app/about/prtl.about.htm', controller: 'AboutController' }
                    }
                })
                .state('public.examine',
                {
                    abstract: true,
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/examine/prtl.examine.htm',
                            controller: 'ExamineController'
                        }
                    }
                })
                .state('public.examine.skeleton',
                {
                    url: "/examine",
                    views: {
                        "examine-app-view": {
                            templateUrl: '/app/examine/skeleton/prtl.examine.skeleton.htm',
                            controller: 'ExamineSkeletonController'
                        }
                    }
                })
                .state('public.examine.symptoms',
                {
                    url: "/examine/symptoms",
                    views: {
                        "examine-app-view": {
                            templateUrl: '/app/examine/symptoms/prtl.examine.symptoms.htm',
                            controller: 'ExamineSymptomsController'
                        }
                    }
                })
                .state('public.examine.report',
                {
                    url: "/examine/report",
                    views: {
                        "examine-app-view": {
                            templateUrl: '/app/examine/report/prtl.examine.report.htm',
                            controller: 'ExamineReportController'
                        }
                    }
                })
                .state('public.research',
                {
                    abstract: true,
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/research/prtl.research.htm',
                            controller: 'ResearchController'
                        },
                    }
                })
                .state('public.research.nav',
                {
                    url: "/research",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/prtl.research.nav.htm'
                        }
                    }
                })
                .state('public.research.plans',
                {
                    url: "/research/plans",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/plans/prtl.research.plans.htm',
                            controller: 'PlanController'
                        }
                    }
                })
                .state('public.research.injuries',
                {
                    url: "/research/injuries",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/injuries/prtl.research.injuries.htm',
                            controller: 'InjuryController'
                        }
                    }
                })
                .state('public.research.locations',
                {
                    url: "/research/locations",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/prtl.research.locations.htm',
                            controller: 'ResearchLocateController'
                        }
                    }
                })
                .state('public.research.exercises',
                {
                    url: "/research/exercises",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/exercises/prtl.research.exercises.htm',
                            controller: 'ExerciseListingController'
                        }
                    }
                })
                .state('public.research.exerciseDetail',
                {
                    url: "/research/exercises/:exerciseId",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/exercises/detail/prtl.exercise.view.htm',
                            controller: 'ExerciseViewController'
                        }
                    }
                })
                .state('public.research.planDetail',
                {
                    url: "/research/plans/:planId",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/plans/detail/prtl.plan.view.htm',
                            controller: 'PlanViewController'
                        }
                    }
                })
                .state('public.research.injuryDetail',
                {
                    url: "/research/injuries/:injuryId",
                    views: {
                        "research-app-view": {
                            templateUrl: '/app/research/injuries/detail/prtl.injury.view.htm',
                            controller: 'InjuryViewController'
                        }
                    }
                })
                .state('patient',
                {
                    abstract: true,
                    views: {
                        "core-view": { templateUrl: '/app/prtl.core.htm' }
                    },
                    data: {
                        access: 'access.user'
                    }
                })
                .state('patient.dashboard',
                {
                    url: "/patient",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/patient/prtl.patient.dashboard.htm',
                            controller: 'PatientDashboardController'
                        }
                    }
                })
                .state('patient.episode',
                {
                    url: "/patient/episode/:episodeId",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/patient/prtl.patient.episode.htm',
                            controller: 'PatientEpisodeController'
                        }
                    }
                })
                .state('patient.episode.session',
                {
                    url: "/session/:sessionId",
                    views: {
                        "patient-session-view": {
                            templateUrl: '/app/patient/prtl.patient.session.htm',
                            controller: 'PatientSessionController'
                        }
                    }
                })
                .state('patient.episode.session.plan',
                {
                    url: "/plan/:planId",
                    views: {
                        "patient-session-plan-view": {
                            templateUrl: '/app/research/plans/detail/prtl.plan.view.htm',
                            controller: 'PlanViewController'
                        }
                    }
                })
                .state('therapist',
                {
                    abstract: true,
                    views: {
                        "core-view": { templateUrl: '/app/prtl.core.htm' }
                    },
                    data: {
                        access: 'access.therapist'
                    }
                })
                .state('therapist.dashboard',
                {
                    url: "/therapist",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/therapist/prtl.therapist.dashboard.htm',
                            controller: 'TherapistDashboardController'
                        }
                    }
                })
                .state('therapist.episode',
                {
                    url: "/therapist/episode/:episodeId",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/therapist/prtl.therapist.episode.htm',
                            controller: 'TherapistEpisodeController'
                        }
                    }
                })
                .state('therapist.session',
                {
                    url: "/therapist/session/:sessionId",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/therapist/prtl.therapist.session.htm',
                            controller: 'TherapistSessionController'
                        }
                    }
                })
                .state('registration',
                {
                    abstract: true,
                    views: {
                        "core-view": { templateUrl: '/app/prtl.core.htm' }
                    },
                    data: {
                        access: 'access.anon'
                    }
                })
                .state('registration.patient',
                {
                    url: "/registration/patient?errId",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/registration/prtl.registration.patient.htm',
                            controller: 'RegistrationPatientController'
                        }
                    }
                })
                .state('registration.therapist',
                {
                    url: "/registration/therapist?errId",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/registration/prtl.registration.therapist.htm',
                            controller: 'RegistrationTherapistController'
                        }
                    }
                })
                .state('clinic',
                {
                    abstract: true,
                    views: {
                        "core-view": { templateUrl: '/app/prtl.core.htm' }
                    },
                    data: {
                        access: 'access.clinic.manager'
                    }
                })
                .state('clinic.dashboard',
                {
                    url: "/clinic/:clinicId/dashboard",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/clinic/prtl.clinic.dashboard.htm',
                            controller: 'ClinicDashboardController'
                        }
                    }
                })
                .state('clinic.manager',
                {
                    url: "/clinic/manager",
                    views: {
                        "core-app-view": {
                            templateUrl: '/app/clinic/prtl.clinic.manager.htm',
                            controller: 'ClinicManagerController'
                        }
                    }
                })
            ;

            $urlRouterProvider.otherwise('/404');
            $locationProvider.html5Mode(true);
            $locationProvider.hashPrefix('!');

        //NOTE: this may impact CORS requests, but we should not have any
        //https://github.com/angular/angular.js/commit/3a75b1124d062f64093a90b26630938558909e8d
        $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

        $httpProvider.interceptors.push(['$q', '$location', function ($q, $location) {
            return {
                'responseError': function(response) {
                    if (response.status === 401 || response.status === 403) {
                        $location.path('/');
                        return $q.reject(response);
                    } else {
                        return $q.reject(response);
                    }
                }
            };
        }]);

        // This is for using $rootScope.$emit and $scope.$on as a global event bus.
        // Creates an $onRootScope method that you can call from a local $scope to bind to events.
        // This will automatically take care of destroying the event binding if the $scope is destroyed.
        // See: http://stackoverflow.com/a/19498009


        $provide.decorator('$rootScope', [
            '$delegate', function ($delegate) {

                Object.defineProperty($delegate.constructor.prototype, '$onRootScope', {
                    value: function (name, listener) {
                        var unsubscribe = $delegate.$on(name, listener);
                        this.$on('$destroy', unsubscribe);
                    },
                    enumerable: false
                });

                return $delegate;
            }
        ]);

    }])
    .run(['Analytics', 'userManagementService', '$rootScope', '$location', function (Analytics, userManagementService, $rootScope, $location) {
        // In case you are relying on automatic page tracking, you need to inject Analytics
        // at least once in your application (for example in the main run() block)

    $rootScope.$on('$stateChangeStart', function(ev, to, toParams, from, fromParams) {

        if (userManagementService.getUser()) {
            userManagementService.getUser().$promise.then(function(user) {
                $rootScope.currentUser = user;
                if (to.data && to.data.access) {
                    var accessLevel = to.data.access.toLowerCase();
                    if (accessLevel !== 'access.anon') {
                        if ($rootScope.currentUser) {
                            if (accessLevel === 'access.admin' && (!$rootScope.currentUser.isAdmin)) {
                                //todo: change this to a access denied page
                                $location.path('/');
                            }
                            if (accessLevel === 'access.therapist' && (!$rootScope.currentUser.isTherapist)) {
                                $location.path('/');
                            }
                            if (accessLevel === 'access.clinic.manager' && (!$rootScope.currentUser.isClinicManager)) {
                                $location.path('/');
                            }
                        } else {
                            event.preventDefault();
                            window.location.assign('/auth?returnUrl=' + encodeURIComponent('#' + to.url));
                        }
                    } 
                }
            });
        };
    });

}]);


var jQueryPluginModule = angular.module('jquery.plugin.module', []);


swptApp.factory('$exceptionHandler', ['notifierService', function (notifierService) {
    return function (exception) {
        notifierService.error(exception.message);
        console.log("exception handled: " + exception.message);
        console.log("exception handled: " + exception.stack);
    };
}]);




// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

var scrollToPlans = function () {
    var elementId = '#recovery-plans';

    if (window.location.href.indexOf(elementId) === -1) {
        window.location.href = window.location.href + elementId;
    } 
};







