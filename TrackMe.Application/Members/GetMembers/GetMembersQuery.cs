using Application.Common.Messaging;

namespace Application.Members.GetMembers;

public sealed record GetMembersQuery : IQuery<IReadOnlyList<MemberResponse>>;

