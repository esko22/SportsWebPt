﻿using System;
using SportsWebPt.Common.ServiceStackClient;

namespace SportsWebPt.Platform.Web.Core
{
    public class SportsWebPtClientSettings : BaseServiceStackClientSettings
    {

        #region Properties
        
        public String WorkoutPath { get; private set; }
        public String EquipmentPath { get; private set; }
        public String VideoPath { get; private set; }
        public String SkeletonAreasUriPath { get; private set; }
        public String BodyPartUriPath { get; private set; }
        public String BodyRegionUriPath { get; private set; }
        public String SymptomaticRegionUriPath { get; private set; }
        public String PotentialSymptomUriPath { get; private set; }
        public String DiffDiagUriPath { get; private set; }
        public String DiagnosisReport { get; private set; }
        public String UserUriPath { get; private set; }
        public String AuthUriPath { get; private set; }
        public String ExercisePath { get; private set; }

        #endregion        
        
        #region Construction

        public SportsWebPtClientSettings()
        {
            WorkoutPath = String.Format("/{0}/workouts", Version);
            EquipmentPath = String.Format("/{0}/equipment", Version);
            VideoPath = String.Format("/{0}/videos", Version);

            SkeletonAreasUriPath = String.Format("/{0}/areas", Version);
            BodyPartUriPath = String.Format("/{0}/bodyparts", Version);
            BodyRegionUriPath = String.Format("/{0}/bodyregions", Version);
            SymptomaticRegionUriPath = String.Format("/{0}/symptomaticregions", Version);
            PotentialSymptomUriPath = String.Format("/{0}/potentialsymptoms", Version);
            DiffDiagUriPath = String.Format("/{0}/differentialdiagnosis", Version);
            DiagnosisReport = String.Format("/{0}/diagnosisreports", Version);

            UserUriPath = String.Format("/{0}/users", Version);
            AuthUriPath = String.Format("/{0}/auth", Version);
            ExercisePath = String.Format("/{0}/exercises", Version); 

        }

        #endregion
    }
}