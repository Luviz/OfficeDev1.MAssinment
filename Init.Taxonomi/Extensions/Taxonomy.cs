using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Init.Taxonomi.Extensions {
	public static class Taxonomy {

		public static TermGroup GenerateGroup(this TermStore ts, string name, string guid) {
			return GenerateGroup(ts, name, new Guid(guid));
		}
		public static TermGroup GenerateGroup(this TermStore ts, string name, Guid guid) {
			var group = ts.Groups.FirstOrDefault(g => g.Name == name);
			if (group == null) 
				group = ts.CreateGroup(name, guid);
			return group;
		}

		public static TermSet GenerateTermSet(this TermGroup group, string name, string guid, int lcid = 1033) {
			return GenerateTermSet(group, name, new Guid(guid));
		}
		public static TermSet GenerateTermSet(this TermGroup group, string name, Guid guid, int lcid = 1033) {
			group.Context.Load(group.TermSets);
			group.Context.ExecuteQuery();
			var termSet = group.TermSets.FirstOrDefault(ts => ts.Name == name);
			if (termSet == null) 
				termSet = group.CreateTermSet(name, guid, lcid);
			return termSet;
		}


		public static Term AddTerm(this TermSet ts, string name, string guid, int lcid = 1033) {
			return AddTerm(ts, name, new Guid(guid), lcid);
		}
		public static Term AddTerm(this TermSet ts, string name, Guid guid = new Guid(), int lcid = 1033) {
			ts.Context.Load(ts.Terms);
			ts.Context.ExecuteQuery();
			var term = ts.Terms.FirstOrDefault(t => string.Compare(t.GetDefaultLabel(lcid).Value, name) == 0);
			if (term == null)
				term = ts.CreateTerm(name, lcid, guid);
			return term;
		}
		//this is having a hickup!
		public static Term AddTerm(this Term term, string name, Guid guid = new Guid(), int lcid = 1033) {
			term.Context.Load(term.Terms);
			term.Context.ExecuteQuery();
			var newterm = term.Terms.FirstOrDefault(t => string.Compare(t.GetDefaultLabel(lcid).Value, name) == 0);
			if (newterm == null)
				newterm = term.CreateTerm(name, lcid, guid );
			return newterm;
		}
	}
}
