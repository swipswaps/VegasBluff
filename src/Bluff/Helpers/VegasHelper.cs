using System;
using System.Collections.Generic;
using Sony.Vegas;

namespace Bluff.Helpers
{
    public static class VegasHelper
    {
        public static List<T> GetTracks<T>(Vegas vegas, int minTracks = 0, int maxTracks = Int32.MaxValue, bool onlySelected = false) where T : Track
        {
            var selectedTracks = new List<T>();
            foreach (Track track in vegas.Project.Tracks)
            {
                if ((!onlySelected || track.Selected) && track is T)
                {
                    selectedTracks.Add((T)track);
                }
            }

            CheckNumberOfTracks(selectedTracks, minTracks, maxTracks);

            return selectedTracks;
        }
        
        private static void CheckNumberOfTracks<T>(List<T> selectedTracks, int minTracks, int maxTracks) where T : Track
        {

            if (selectedTracks.Count < minTracks)
            {
                throw new BluffException(String.Format("Must have at lease {0} video track selected.", minTracks));
            }

            if (selectedTracks.Count > maxTracks)
            {
                throw new BluffException(String.Format("Must have less than {0} video track selected.", maxTracks));
            }

        }

        public static List<TrackEvent> GetSelectedTrackEvents(List<VideoTrack> videoTracks)
        {
            var selectedTrackEvents = new List<TrackEvent>();

            foreach (var selectedTrack in videoTracks)
            {
                foreach (var trackEvent in selectedTrack.Events)
                {
                    if (trackEvent.Selected)
                    {
                        selectedTrackEvents.Add(trackEvent);
                    }
                }
            }

            if (selectedTrackEvents.Count == 0)
            {
                throw new BluffException(String.Format("Must have events selected."));
            }
            return selectedTrackEvents;
        }
    }
}