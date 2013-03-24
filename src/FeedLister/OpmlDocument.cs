﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


//NOTE: Based on specs at: http://dev.opml.org/spec1.html and http://dev.opml.org/spec2.html


namespace FeedLister
{
    public class OpmlDocument
    {
        public Head Head { get; private set; }
        public Body Body { get; private set; }
        public string Version { get; private set; } // Required.



        private OpmlDocument(string version, Head head, Body body)
        {
            this.Version = version;
            this.Head = head;
            this.Body = body;
        }



        public static OpmlDocument Create(XDocument opml)
        {
            if(opml.Root == null || opml.Root.Name != "opml")
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. Missing 'opml' root-tag.");
            }

            var opmlTag = opml.Root;
            var opmlVersionAttribute = opmlTag.Attributes("version").FirstOrDefault();
            if(opmlVersionAttribute == null)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. Missing 'version' attribute on the 'opml' tag.");
            }

            var opmlVersion = opmlVersionAttribute.Value;
            Regex versionRegex = new Regex(@"^\d\.\d$");
            if(versionRegex.IsMatch(opmlVersion) == false)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. The 'version' attribute on the 'opml' tag contains an invalid version.");
            }

            var headTagList = opmlTag.Descendants("head");
            var headTagCount = headTagList.Count();
            if(headTagCount == 0)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. Missing 'head' tag under the 'opml' tag.");
            }

            if(headTagCount > 1)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. More than 1 'head' tag under the 'opml' tag.");
            }

            var headTag = headTagList.First();
            //TODO: Parse the <head>-tag.

            var bodyTagList = opmlTag.Descendants("body");
            var bodyTagCount = bodyTagList.Count();
            if (bodyTagCount == 0)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. Missing 'body' tag under the 'opml' tag.");
            }

            if (bodyTagCount > 1)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. More than 1 'body' tag under the 'opml' tag.");
            }

            var bodyTag = bodyTagList.First();
            //TODO: Parse the <body>-tag.

            var opmlDocument = new OpmlDocument(opmlVersion, null, null);
            return opmlDocument;
        }
    }
}