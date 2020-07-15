using System;
using System.Globalization;

namespace SpotifyApi.NetCore
{
    internal static class UriBuilderExtensions
    {
        public static void AppendToQuery(this UriBuilder builder, string name, int value)
            => AppendToQuery(builder, name, value.ToString());

        public static void AppendToQuery(this UriBuilder builder, string name, long value)
            => AppendToQuery(builder, name, value.ToString());

        public static void AppendToQuery(this UriBuilder builder, string name, string value)
        {
            if (string.IsNullOrEmpty(builder.Query)) builder.Query = $"{name}={value}";
            else builder.Query = $"{builder.Query.Substring(1)}&{name}={value}";
        }

        public static void AppendToQueryAsCsv(this UriBuilder builder, string name, string[] values)
        {
            if (values != null && values.Length > 0)
                AppendToQuery(builder, name, string.Join(",", values));
        }

        public static void AppendToQueryIfValueGreaterThan0(
            this UriBuilder builder,
            string name,
            long? value)
        {
            if (value.HasValue) AppendToQueryIfValueGreaterThan0(builder, name, value.Value);
        }

        public static void AppendToQueryIfValueGreaterThan0(
            this UriBuilder builder,
            string name,
            long value)
        {
            if (value > 0) AppendToQuery(builder, name, value);
        }

        public static void AppendToQueryIfValueGreaterThan0(
            this UriBuilder builder,
            string name,
            int? value)
        {
            if (value.HasValue) AppendToQueryIfValueGreaterThan0(builder, name, value.Value);
        }

        public static void AppendToQueryIfValueGreaterThan0(
            this UriBuilder builder,
            string name,
            int value)
        {
            if (value > 0) AppendToQuery(builder, name, value);
        }

        public static void AppendToQueryIfValueNotNullOrWhiteSpace(
            this UriBuilder builder,
            string name,
            string value)
        {
            if (!string.IsNullOrWhiteSpace(value)) AppendToQuery(builder, name, value);
        }

        public static void AppendToQueryAsTimestampIso8601(this UriBuilder builder, string name, DateTime? timestamp)
        {
            if (timestamp.HasValue) AppendToQuery(builder, name, timestamp.Value.ToString("s", CultureInfo.InvariantCulture));
        }
    }
}
