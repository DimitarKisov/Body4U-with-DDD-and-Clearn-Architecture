﻿namespace Body4U.Domain.Models.Trainers
{
    using Body4U.Domain.Common;
    using Body4U.Domain.Exceptions;
    using Body4U.Domain.Models.Articles;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Body4U.Domain.Models.ModelContants.Trainer;

    public class Trainer : Entity<int>
    {
        private readonly HashSet<Article> articles;
        private readonly HashSet<TrainerImage> trainerImages;
        private readonly HashSet<TrainerVideo> trainerVideos;

        internal Trainer(
            string bio,
            string shortBio,
            string facebookUrl,
            string instagramUrl,
            string youtubeChannelUrl)
        {
            this.Validate(bio, shortBio, facebookUrl, instagramUrl, youtubeChannelUrl);

            this.Bio = bio;
            this.ShortBio = shortBio;
            this.FacebookUrl = facebookUrl;
            this.InstagramUrl = instagramUrl;
            this.YoutubeChannelUrl = youtubeChannelUrl;
            this.CreatedOn = DateTime.Now;

            this.articles = new HashSet<Article>();
            this.trainerImages = new HashSet<TrainerImage>();
            this.trainerVideos = new HashSet<TrainerVideo>();
        }

        public string Bio { get; private set; }

        public string ShortBio { get; private set; }

        public string FacebookUrl { get; private set; }

        public string InstagramUrl { get; private set; }

        public string YoutubeChannelUrl { get; private set; }

        public bool IsReadyToVisualize { get; private set; } = default;

        public bool IsReadyToWrite { get; private set; } = default;

        public DateTime CreatedOn { get; }

        public DateTime? ModifiedOn { get; private set; }

        public string ModifiedBy { get; private set; } = default!;

        public IEnumerable<Article> Articles => this.articles.ToList().AsReadOnly();

        public IEnumerable<TrainerImage> TrainerImages => this.trainerImages.ToList().AsReadOnly();

        public IEnumerable<TrainerVideo> TrainerVideos => this.trainerVideos.ToList().AsReadOnly();

        #region State mutation methods
        public Trainer UpdateBio(string bio, string userId)
        {
            this.ValidateBio(bio);

            this.Bio = bio;
            Modification(userId);
            return this;
        }

        public Trainer UpdateShortBio(string shortBio, string userId)
        {
            this.ValidateShortBio(shortBio);

            this.ShortBio = shortBio;
            Modification(userId);
            return this;
        }

        public Trainer UpdateFacebookUrl(string facebookUrl, string userId)
        {
            this.ValidateFacebookUrl(facebookUrl);

            this.FacebookUrl = facebookUrl;
            Modification(userId);
            return this;
        }

        public Trainer UpdateInstagramUrl(string instagramUrl, string userId)
        {
            this.ValidateInstagramUrl(instagramUrl);

            this.InstagramUrl = instagramUrl;
            Modification(userId);
            return this;
        }

        public Trainer UpdateYoutubeChannelUrl(string youtubeChannelUrl, string userId)
        {
            this.ValidateYoutubeChannelUrl(youtubeChannelUrl);

            this.YoutubeChannelUrl = youtubeChannelUrl;
            Modification(userId);
            return this;
        }

        public Trainer ChangeVisibility()
        {
            this.IsReadyToVisualize = !this.IsReadyToVisualize;
            return this;
        }

        public Trainer ChangeOpportunityToWrite()
        {
            this.IsReadyToWrite = !this.IsReadyToWrite;
            return this;
        }

        private void Modification(string userId)
        {
            this.ModifiedOn = DateTime.Now;
            this.ModifiedBy = userId;
        }
        #endregion

        #region Validations
        private void Validate(string bio, string shortBio, string facebookUrl, string instagramUrl, string youtubeChannelUrl)
        {
            Guard.AgainstEmptyString<InvalidTrainerException>(bio, nameof(this.Bio));

            Guard.ForStringLength<InvalidTrainerException>(bio, MinBioLength, MaxBioLength, nameof(this.Bio));

            Guard.AgainstEmptyString<InvalidTrainerException>(shortBio, nameof(this.ShortBio));

            Guard.ForStringLength<InvalidTrainerException>(shortBio, MinShortBioLenght, MaxShortBioLength, nameof(this.ShortBio));

            Guard.AgaintsWrongUrl<InvalidTrainerException>(facebookUrl, FacebookUrlRegex, nameof(this.FacebookUrl));

            Guard.AgaintsWrongUrl<InvalidTrainerException>(instagramUrl, InstragramUrlRegex, nameof(this.InstagramUrl));

            Guard.AgaintsWrongUrl<InvalidTrainerException>(youtubeChannelUrl, YoutubeChannelUrlRegex, nameof(this.YoutubeChannelUrl));
        }

        private void ValidateBio(string bio)
        {
            Guard.AgainstEmptyString<InvalidTrainerException>(bio, nameof(this.Bio));

            Guard.ForStringLength<InvalidTrainerException>(bio, MinBioLength, MaxBioLength, nameof(this.Bio));
        }

        private void ValidateShortBio(string shortBio)
        {
            Guard.AgainstEmptyString<InvalidTrainerException>(shortBio, nameof(this.ShortBio));

            Guard.ForStringLength<InvalidTrainerException>(shortBio, MinShortBioLenght, MaxShortBioLength, nameof(this.ShortBio));
        }

        private void ValidateFacebookUrl(string facebookUrl) 
            => Guard.AgaintsWrongUrl<InvalidTrainerException>(facebookUrl, FacebookUrlRegex, nameof(this.FacebookUrl));

        private void ValidateInstagramUrl(string instagramUrl)
            => Guard.AgaintsWrongUrl<InvalidTrainerException>(instagramUrl, InstragramUrlRegex, nameof(this.InstagramUrl));

        private void ValidateYoutubeChannelUrl(string youtubeChannelUrl)
            => Guard.AgaintsWrongUrl<InvalidTrainerException>(youtubeChannelUrl, YoutubeChannelUrlRegex, nameof(this.YoutubeChannelUrl));
        #endregion
    }
}
