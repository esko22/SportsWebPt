define('model.diagnosis.report', ['backbone', 'config', 'model.injury', 'model.injury.collection' ],
    function (backbone, config, Injury, InjuryCollection ) {
        var
            diagnosisReport = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.diagnosisReport,
                defaults: {
                },
                idAttribute: 'differenetialDiagnosisId',
                relations: [{
                    type: backbone.HasMany,
                    key: 'potentialInjuries',
                    relatedModel: Injury,
                    collectionType: InjuryCollection,
                    reverseRelation: {
                        key: 'diagnosisReportId',
                        includeInJSON: 'differenetialDiagnosisId'
                    }
                }]
            });

        return diagnosisReport;

    });

define('model.diagnosis.report.collection', ['backbone', 'model.diagnosis.report'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });