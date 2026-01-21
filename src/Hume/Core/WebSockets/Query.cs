using System.Collections;
using Hume.EmpathicVoice;
using OneOf;

namespace Hume.Core.WebSockets;

/// <summary>
/// Represents a collection of query parameters that can be used to build URL query strings.
/// Provides type-safe methods for adding various parameter types and supports enumeration.
/// </summary>
public class Query : IEnumerable<KeyValuePair<string, string>>
{
    private readonly List<KeyValuePair<string, string>> _queryParameters = [];

    /// <summary>
    /// Initializes a new instance of the Query class.
    /// </summary>
    public Query() { }

    /// <summary>
    /// Creates a new empty Query instance.
    /// </summary>
    /// <returns>A new Query instance.</returns>
    public static Query Create() => [];

    /// <summary>
    /// Adds a string parameter to the query if both key and value are not null or empty.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value!));
    }

    /// <summary>
    /// Adds a boolean parameter to the query if both key and value are not null.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, bool? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value.ToString()!.ToLower()));
    }

    /// <summary>
    /// Adds an integer parameter to the query if both key and value are not null.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, int? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value.ToString()!));
    }

    /// <summary>
    /// Adds a float parameter to the query if both key and value are not null.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, float? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value.ToString()!));
    }

    /// <summary>
    /// Adds a double parameter to the query if both key and value are not null.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, double? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value.ToString()!));
    }

    /// <summary>
    /// Adds a decimal parameter to the query if both key and value are not null.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, decimal? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value.ToString()!));
    }

    /// <summary>
    /// Adds ConnectSessionSettings to the query using deep object notation.
    /// Example: session_settings[system_prompt]=value, session_settings[variables][name]=John
    /// </summary>
    /// <param name="key">The parameter key (e.g., "session_settings").</param>
    /// <param name="value">The ConnectSessionSettings value.</param>
    public void Add(string key, ConnectSessionSettings? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }

        // Add each property using deep object notation
        if (value.SystemPrompt != null)
        {
            _queryParameters.Add(new KeyValuePair<string, string>($"{key}[system_prompt]", value.SystemPrompt));
        }

        if (value.CustomSessionId != null)
        {
            _queryParameters.Add(new KeyValuePair<string, string>($"{key}[custom_session_id]", value.CustomSessionId));
        }

        if (value.Audio != null)
        {
            if (value.Audio.Encoding != null)
            {
                _queryParameters.Add(new KeyValuePair<string, string>($"{key}[audio][encoding]", value.Audio.Encoding.ToString()!));
            }
            if (value.Audio.Channels != null)
            {
                _queryParameters.Add(new KeyValuePair<string, string>($"{key}[audio][channels]", value.Audio.Channels.ToString()!));
            }
            if (value.Audio.SampleRate != null)
            {
                _queryParameters.Add(new KeyValuePair<string, string>($"{key}[audio][sample_rate]", value.Audio.SampleRate.ToString()!));
            }
        }

        if (value.Context != null)
        {
            if (value.Context.Text != null)
            {
                _queryParameters.Add(new KeyValuePair<string, string>($"{key}[context][text]", value.Context.Text));
            }
            if (value.Context.Type != null)
            {
                _queryParameters.Add(new KeyValuePair<string, string>($"{key}[context][type]", value.Context.Type.ToString()!));
            }
        }

        if (value.EventLimit != null)
        {
            _queryParameters.Add(new KeyValuePair<string, string>($"{key}[event_limit]", value.EventLimit.ToString()!));
        }

        if (value.LanguageModelApiKey != null)
        {
            _queryParameters.Add(new KeyValuePair<string, string>($"{key}[language_model_api_key]", value.LanguageModelApiKey));
        }

        if (value.VoiceId != null)
        {
            _queryParameters.Add(new KeyValuePair<string, string>($"{key}[voice_id]", value.VoiceId));
        }

        // Handle variables with nested deep object notation
        if (value.Variables != null)
        {
            foreach (var variable in value.Variables)
            {
                var varValue = variable.Value.Match(
                    str => str,
                    dbl => dbl.ToString(),
                    b => b.ToString().ToLower()
                );
                _queryParameters.Add(new KeyValuePair<string, string>($"{key}[variables][{variable.Key}]", varValue));
            }
        }
    }

    /// <summary>
    /// Adds an object parameter to the query if both key and value are not null.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void Add(string key, Object? value)
    {
        if (value == null || string.IsNullOrEmpty(key))
        {
            return;
        }
        _queryParameters.Add(new KeyValuePair<string, string>(key, value.ToString()!));
    }

    /// <summary>
    /// Converts the query parameters to a URL-encoded query string.
    /// </summary>
    /// <returns>A string representation of the query parameters in the format "key1=value1&key2=value2".</returns>
    public override string ToString()
    {
        return string.Join(
            "&",
            _queryParameters.Select(kvp =>
                $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"
            )
        );
    }

    /// <summary>
    /// Returns an enumerator that iterates through the query parameters.
    /// </summary>
    /// <returns>An enumerator for the query parameters.</returns>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _queryParameters.GetEnumerator();
    }

    /// <summary>
    /// Implicitly converts a Query instance to its string representation.
    /// </summary>
    /// <param name="queryBuilder">The Query instance to convert.</param>
    /// <returns>The string representation of the query parameters.</returns>
    public static implicit operator string(Query queryBuilder)
    {
        return queryBuilder.ToString();
    }

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the query parameters.
    /// </summary>
    /// <returns>A non-generic enumerator for the query parameters.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
