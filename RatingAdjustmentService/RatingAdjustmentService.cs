﻿using System;

namespace RatingAdjustment.Services
{
    /** Service calculating a star rating accounting for the number of reviews
     * 
     */
    public class RatingAdjustmentService
    {
        const double MAX_STARS = 5.0;  // Likert scale
        const double Z = 1.96; // 95% confidence interval

        double _q;
        double _percent_positive;

        /** Percentage of positive reviews
         * 
         * In this case, that means X of 5 ==> percent positive
         * 
         * Returns: [0, 1]
         */
        void SetPercentPositive(double stars)
        {
            _percent_positive = stars / 5;
        }

        /**
         * Calculate "Q" given the formula in the problem statement
         */
        void SetQ(double number_of_ratings)
        {
            double a = _percent_positive * (1 - _percent_positive);
            double b = (Z * Z) / (4 * number_of_ratings);
            double c = (a + b) / number_of_ratings;

            _q = Z * Math.Sqrt(c);
        }

        /** Adjusted lower bound
         * 
         * Lower bound of the confidence interval around the star rating.
         * 
         * Returns: a double, up to 5
         */
        public double Adjust(double stars, double number_of_ratings)
        {
            _percent_positive = stars / 5;
            double a = _percent_positive * (1 - _percent_positive);
            double b = (Z * Z) / (4 * number_of_ratings);
            double c = (a + b) / number_of_ratings;
            _q = Z * Math.Sqrt(c);
            double last_val = _percent_positive + ((Z * Z) / (2 * number_of_ratings)) - _q;
            double last_val1 = (1 + ((Z * Z) / number_of_ratings));
            double final = (last_val / last_val1) * 5;
            return Math.Round(final,2);
        }
    }
}
